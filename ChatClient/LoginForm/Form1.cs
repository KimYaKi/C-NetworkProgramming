using ListForm;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class lgForm : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        Socket mainSock;
        IPAddress thisAddress = IPAddress.Parse("210.123.254.197");
        string nameID;

        public lgForm()
        {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (mainSock.Connected)
            {
                MsgBoxHelper.Error("이미 연결되어 있습니다!");
                return;
            }

            int port = 15000;  //고정

            nameID = id_text.Text.Trim(); //ID
            if (string.IsNullOrEmpty(nameID))
            {
                MsgBoxHelper.Warn("ID가 입력되지 않았습니다!");
                id_text.Focus();
                return;
            }

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

            // Login 정보
            string login_code = "200/" + id_text.Text + "/" + pwd_text.Text + "/";
            // 서버로 ID 전송
            SendID(login_code);

            // 연결 완료, 서버에서 데이터가 올 수 있으므로 수신 대기한다.
            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
        }

        void SendID(string protocol)
        {
            try
            {
                // 문자열을 utf8 형식의 바이트로 변환한다.
                byte[] bDts = Encoding.UTF8.GetBytes(protocol);

                // 서버에 전송한다.
                mainSock.Send(bDts);

            }catch(Exception e)
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
            if (state_code.Equals("201")){
                MessageBox.Show("로그인 성공");
                // 텍스트박스에 추가해준다.
                // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
                // 따라서 대리자를 통해 처리한다.


                // 클라이언트에선 데이터를 전달해줄 필요가 없으므로 바로 수신 대기한다.
                // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
                obj.ClearBuffer();

                // 수신 대기
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
                //this.Hide();
                //new lsForm(id_text.Text,mainSock).ShowDialog();
                //this.ShowDialog();
            }
            else
            {
                MessageBox.Show("로그인 실패");
                mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            }
        }

        private void lgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string data = "500/" + id_text.Text + "/";
            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes(data);

            Console.WriteLine(data);
            if (mainSock.Connected)
            {
                // 서버에 전송한다.
                mainSock.Send(bDts);
            }
        }
    }
}
