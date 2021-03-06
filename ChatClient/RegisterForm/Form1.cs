﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace RegisterForm
{
    public partial class regForm : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        Socket mainSock;
        IPAddress thisAddress = IPAddress.Parse("210.123.254.197");

        public regForm()
        {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(inputID.Text.Equals("") || inputPwd.Text.Equals("") || inputName.Text.Equals(""))
            {
                MessageBox.Show("빈칸이 있습니다.");
            }
            else
            {
                if (mainSock.Connected)
                {
                    MsgBoxHelper.Error("이미 연결되어 있습니다!");
                    return;
                }
                int port = 15000;  //고정

                // 서버에 연결
                try
                {
                    mainSock.Connect("210.123.254.197", port);
                }
                catch (Exception ex)
                {
                    mainSock.Close();
                    MsgBoxHelper.Error("연결에 실패했습니다!\n오류 내용: {0}", MessageBoxButtons.OK, ex.Message);
                    return;
                }
                // 회원가입 정보
                string data = "300/" + inputID.Text + "/" + inputPwd.Text + "/" + inputName.Text + "/";
                // 서버로 ID 전송
                SendID(data);
                // 연결 완료, 서버에서 데이터가 올 수 있으므로 수신 대기한다.
                AsyncObject obj = new AsyncObject(4096);
                obj.WorkingSocket = mainSock;
                mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
            }
        }

        void SendID(string protocol)
        {
            try
            {
                string data = AES_encrypt(protocol, "01234567890123456789012345678901");
                // 문자열을 utf8 형식의 바이트로 변환한다.
                byte[] bDts = Encoding.UTF8.GetBytes(data);
                MessageBox.Show(data);
                // 서버에 전송한다.
                mainSock.Send(bDts);
            }
            catch (Exception e)
            {
                mainSock.Close();
                MessageBox.Show("다시 실행해주세요.\n예외 : " + e.StackTrace.ToString());
            }
        }
        void DataReceived(IAsyncResult ar)
        {
            // BeginReceive에서 추가적으로 넘어온 데이터를 AsyncObject 형식으로 변환한다.
            AsyncObject obj = (AsyncObject)ar.AsyncState;
            // 데이터 수신을 끝낸다.
            int received = obj.WorkingSocket.EndReceive(ar);
            // 받은 데이터가 없으면(연결끊어짐) 끝낸다.
            if (received <= 0)
            {
                obj.WorkingSocket.Disconnect(false);
                obj.WorkingSocket.Close();
                return;
            }
            // 텍스트로 변환한다.
            string text = Encoding.UTF8.GetString(obj.Buffer);
            Console.WriteLine(text);
            string[] recv_data = text.Split('/');
            string state_code = recv_data[0];
            if (state_code.Equals("301"))
            {
                MessageBox.Show("회원가입 성공");
                // 텍스트박스에 추가해준다.
                // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
                // 따라서 대리자를 통해 처리한다.
                // 클라이언트에선 데이터를 전달해줄 필요가 없으므로 바로 수신 대기한다.
                // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
                obj.ClearBuffer();
                // 수신 대기
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
            }
            else
            {
                MessageBox.Show("회원가입 실패");
                // 회원가입에 실패하면 Socket이 닫히기 때문에 다시 생성 시켜주어야 한다.
                mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            }
        }
        private void regForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string endMsg = "500/End/";
            string data = AES_encrypt(endMsg, "01234567890123456789012345678901");
            byte[] bDts = Encoding.UTF8.GetBytes(data);
            if (mainSock.Connected)
            {
                mainSock.Close();
            }
        }



        //-------------------------------------------------------------------------------------------------
        // 참고 사이트 https://zoonvivor.tistory.com/18
        //AES256 암호화
        public string AES_encrypt(string Input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes("0123456789012345");

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);

                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
                string recvdata = Encoding.Default.GetString(xBuff);
                
            }

            string Output = Convert.ToBase64String(xBuff);
            return Output;
        }
        //AES 256 복호화
        public string AES_decrypt(string Input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes("0123456789012345");

            var decrypt = aes.CreateDecryptor();
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {

                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(Input);
                    string recvdata = Encoding.Default.GetString(xXml);
                    
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            string Output = Encoding.UTF8.GetString(xBuff);
            return Output;
        }
    }
}
