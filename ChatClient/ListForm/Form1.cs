using ChatClient;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ListForm
{
    public partial class lsForm : Form
    {
        delegate void AppendTextDelegate(ListView ctrl, string[] s);
        AppendTextDelegate _textAppender;

        Socket mainSocket;
        string id;
        string list;
        string[] client;
        public lsForm(string recvId, string recvList, Socket recvSocket)
        {
            InitializeComponent();
            _textAppender = new AppendTextDelegate(AppendText);
            id = recvId;
            mainSocket = recvSocket;
            list = recvList;
            client = list.Split('#');

            AppendText(clt_list, client);

            // 연결 완료, 서버에서 데이터가 올 수 있으므로 수신 대기한다.
            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = mainSocket;
            mainSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
        }

        // 텍스트 추가 메소드 (데이터를 받으면 TextBox에 출력 해 준다.)
        void AppendText(ListView ctrl, string[] s)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke(_textAppender, ctrl, s);
            else
            {
                ctrl.Clear();

                int userNum = 0;

                foreach (var cli in s)
                {
                    if (cli.Equals(id))
                        break;
                    userNum++;
                }

                s = s.Where(condition => condition != s[userNum]).ToArray();
                

                var listViewItem = new ListViewItem(s);
                ctrl.Items.Add(listViewItem);
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
            string before = Encoding.UTF8.GetString(obj.Buffer).Trim('\0');

            // 암호화 된 데이터 복호화 작업
            // Key 값은 32비트 값
            string text = AES_decrypt(before, "01234567890123456789012345678901");
            client = text.Split('#');

            AppendText(clt_list, client);

            // 클라이언트에선 데이터를 전달해줄 필요가 없으므로 바로 수신 대기한다.
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();

            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
            

            //--------------------------------------------------------------------

            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.
            // AppendText(txtHistory, string.Format("[받음]{0}: {1}", id, msg));
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();

            // 수신 대기
            if (mainSocket == null)
            {
                Console.WriteLine("비어있음");
            }
            else if (mainSocket.Connected)
            {
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
            }
        }

        //-------------------------------------------------------------------------------------------------
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

        private void clt_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(clt_list.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = clt_list.SelectedItems;
                ListViewItem lvItem = items[0];
                string toID = lvItem.SubItems[0].Text;
                MessageBox.Show(toID);
                new chForm(id, toID, mainSocket).ShowDialog();
            }
        }
    }
}
