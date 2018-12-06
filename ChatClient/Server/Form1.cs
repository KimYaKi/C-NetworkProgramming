﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class serverForm : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        IPAddress thisAddress;
        Dictionary<string, Socket> connectedClients;
        int clientNum;

        Dictionary<string, string> ClientList;


        public serverForm()
        {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);
            connectedClients = new Dictionary<string, Socket>();
            clientNum = 0; //초기화
            ClientList = new Dictionary<string, string>();
        }

        // 텍스트 추가 메소드 (데이터를 받으면 TextBox에 출력 해 준다.)
        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else
            {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }

        private void OnServerLoaded(object sender, EventArgs e)
        {
            new ClientChk(ClientList);
            string value;
            
            foreach (var id in ClientList.Keys)
            {
                Console.WriteLine("{0} : {1}", id, ClientList[id]);
            }
        }


        void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                // 클라이언트의 연결 요청을 수락한다.
                Socket client = mainSock.EndAccept(ar);

                // 또 다른 클라이언트의 연결을 대기한다.
                mainSock.BeginAccept(AcceptCallback, null);

                AsyncObject obj = new AsyncObject(4096);// 4096 buffer size
                obj.WorkingSocket = client;

                AppendText(txtHistory, string.Format("클라이언트 접속 : @{0}",
                    client.RemoteEndPoint));

                // 클라이언트의 ID 데이터를 받는다.
                client.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
                if (mainSock != null)
                    mainSock.Close();
            }
        }

        // 데이터를 받는 부분
        void DataReceived(IAsyncResult ar)
        {
            // BeginReceive에서 추가적으로 넘어온 데이터를 AsyncObject 형식으로 변환한다.
            AsyncObject obj = (AsyncObject)ar.AsyncState;

            // Socket에 Error가 있는 부분을 위한 변수
            SocketError errorCode;

            // 데이터를 받았을 때 errorCode도 함께 값을 선언한다.
            int received = obj.WorkingSocket.EndReceive(ar, out errorCode);

            // errorCode가 Success가 아니라면 received를 0으로 변환
            if(errorCode != SocketError.Success)
            {
                received = 0;
            }

            // received의 값이 위에서 선언 한 바와 같이 0이라면
            // 해당 if문이 실행 된다.
            if (received <= 0)
            {
                // AppendText(txtHistory, string.Format("클라이언트 접속해제?{0}", clientNum));
                if (clientNum > 0)
                {
                    foreach (KeyValuePair<string, Socket> clients in connectedClients)
                    {
                        if (obj.WorkingSocket == clients.Value)
                        {
                            string key = clients.Key;
                            try
                            {
                                connectedClients.Remove(key);
                                AppendText(txtHistory, string.Format("접속해제완료:{0}", key));
                            }
                            catch { }
                            break;
                        }
                    }
                }
                
                return;
            }
            // 텍스트로 변환한다.
            string before = Encoding.UTF8.GetString(obj.Buffer).Trim('\0');
            
            // 암호화 된 데이터 복호화 작업
            // Key 값은 32비트 값
            string text = AES_decrypt(before, "01234567890123456789012345678901");
            
            // 복호화 된 데이터를 '/'을 기준으로 배열로 분할
            string[] recv_text = text.Split('/');
            // 항상 데이터의 0번째와 1번째는
            // 상태 코드, id가 각각 들어간다.
            string state_code = recv_text[0];
            string id = recv_text[1];

            // 전송받은 데이터에 대한 ACK를 보내기 위한 변수
            string sendingMsg;
            // 기번 버퍼 초기화
            byte[] msgBuff = null;
            
            Socket socket = null;

            //--------------------------------------------------------------------
            switch (state_code)
            {
                // 회원가입
                case "100":
                    
                    break;

                // 로그인
                case "200":
                    string pwd = recv_text[2];
                    // 클라이언트 수 증가
                    clientNum++;
                    AppendText(txtHistory, string.Format("[접속{0}]ID:{1}:{2}",
                               clientNum, id, obj.WorkingSocket.RemoteEndPoint.ToString()));
                    // ID 체크 하는 부분
                    if (id.Equals("test"))
                    {
                        // 리스트에 추가하는 부분은 로그인 시에만 하면 됨
                        connectedClients.Add(id, obj.WorkingSocket);
                        // 현제 id(key)에 해당하는 Socket값을 socket변수에 반환 시켜 줌
                        connectedClients.TryGetValue(id, out socket);
                        // 로그인 성공 메시지
                        sendingMsg = "201/Success/로그인 성공/";
                        // TextBox에 메시지 출력
                        AppendText(txtHistory, sendingMsg);
                        // 전송 할 메시지를 UTF8로 인코딩
                        msgBuff = Encoding.UTF8.GetBytes(sendingMsg);
                        // 현제 Socket값에 메시지 전송
                        sendTo(socket, msgBuff);
                    }
                    else
                    {
                        // 로그인 실패 메시지
                        // 로그인 실패시에는 ID를 Dic에 추가하지 않는다.
                        sendingMsg = "202/Failed/로그인 실패/";
                        // 실패 메시지 출력
                        AppendText(txtHistory, sendingMsg);
                        // 메시지를 UTF8로 인코딩
                        msgBuff = Encoding.UTF8.GetBytes(sendingMsg);
                        // 현제 접속한 Socket에 메시지 전송
                        sendTo(obj.WorkingSocket, msgBuff);
                        // 로그인에 실패한 소켓 값은 닫는다.
                        obj.WorkingSocket.Close();
                    }
                    break;

                // 채팅
                case "300":
                    if (true)
                    {
                        sendingMsg = "301/Success/회원가입 성공/";
                        AppendText(txtHistory, sendingMsg);
                        msgBuff = Encoding.UTF8.GetBytes(sendingMsg);
                        sendTo(obj.WorkingSocket, msgBuff);
                        obj.WorkingSocket.Close();
                    }
                    else
                    {
                        sendingMsg = "302/Failed/회원가입 실패/";
                        AppendText(txtHistory, sendingMsg);
                        msgBuff = Encoding.UTF8.GetBytes(sendingMsg);
                        sendTo(obj.WorkingSocket, msgBuff);
                        obj.WorkingSocket.Close();
                    }
                    break;

                // 종료
                case "500":
                    sendingMsg = "501/End/";
                    connectedClients.TryGetValue(id, out socket);
                    msgBuff = Encoding.UTF8.GetBytes(sendingMsg);
                    connectedClients.Remove(id);
                    AppendText(txtHistory, string.Format("접속해제완료:{0}", id));
                    break;
            }
            //--------------------------------------------------------------------

            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.
            // AppendText(txtHistory, string.Format("[받음]{0}: {1}", id, msg));
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();

            // 수신 대기
            if(socket == null){
                Console.WriteLine("비어있음");
            }
            else if (socket.Connected)
            {
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
            }
        }

        void sendTo(Socket socket, byte[] buffer)
        {
            try
            {
                socket.Send(buffer);
            }
            catch
            {// 오류 발생하면 전송 취소
                try { AppendText(txtHistory, "dispose"); socket.Dispose(); } catch { }
            }
        }

        void sendAll(Socket except, byte[] buffer)
        {
            foreach (Socket socket in connectedClients.Values)
            {
                if (socket != except)
                {
                    try { socket.Send(buffer); }
                    catch
                    {// 오류 발생하면 전송 취소하고 삭제
                        try { socket.Dispose(); } catch { }
                    }
                }
            }
        }

        void OnSendData(object sender, EventArgs e)
        {
            // 서버가 대기중인지 확인한다.
            if (!mainSock.IsBound)
            {
                MsgBoxHelper.Warn("서버가 실행되고 있지 않습니다!");
                return;
            }

            // 보낼 텍스트
            string tts = "ABCD".Trim();
            

            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes("Server" + ':' + tts);

            // 연결된 모든 클라이언트에게 전송한다.
            sendAll(null, bDts);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            AppendText(txtHistory, string.Format("[보냄]server: {0}", tts));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            int port;
            int.TryParse("15000", out port);
            thisAddress = IPAddress.Parse("127.0.0.1");

            // 서버에서 클라이언트의 연결 요청을 대기하기 위해
            // 소켓을 열어둔다.
            IPEndPoint serverEP = new IPEndPoint(thisAddress, port);
            mainSock.Bind(serverEP);
            mainSock.Listen(10);

            AppendText(txtHistory, string.Format("서버 시작: @{0}", serverEP));
            // 비동기적으로 클라이언트의 연결 요청을 받는다.
            mainSock.BeginAccept(AcceptCallback, null);
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
                AppendText(txtHistory, "AES256 : " + recvdata);
                //tbDebug.Text += "\r\n\r\nAES256 ( SEND DATA ) : " + recvdata;
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
                    //tbDebug.Text += "\r\n\r\nAES256 ( RECEIVE DATA ) : " + recvdata;
                    AppendText(txtHistory, "AES256 : " + recvdata);

                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            string Output = Encoding.UTF8.GetString(xBuff);
            return Output;
        }
    }
}
