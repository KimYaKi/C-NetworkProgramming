using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 서버로 작동하는 스레드 생성
            Thread serverThread = new Thread(serverFunc);
            // Background로 돌려서 수행
            serverThread.IsBackground = true;
            serverThread.Start();
            // 5초간 기다리다 Key를 인식한다.
            Thread.Sleep(500);

            Console.WriteLine("종료하려면 아무 키나 누르세요...");
            Console.ReadLine();
        }

        private static void serverFunc(object obj)
        {
            // 서버 소켓(srvSocket) 사용
            // 요청 하는 클라이언트에 대한 서버 소켓을 생성
            using (Socket srvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            // 서버에서 사용할 서버소켓 생성 (IPv4, TCP연결, TCP프로토콜)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 11200);
                // IPEndPoint: IP주소와 포트 번호를 가지고 있는 메소드
                // 서버 주소: Any -> 서버에 포함된 모든 IP를 사용 가능, 11200 포트번호

                srvSocket.Bind(endPoint);
                // Bind: 서버의 IP주소와 포트를 바인드
                srvSocket.Listen(10);
                // Client의 접근을 기다림
                // -> 10: 최대 Client의 수를 10으로 제한

                // 클라이언트 연결 요청 무한 루프
                while (true)
                {
                    // 연결 수락
                    Socket clntSocket = srvSocket.Accept();
                    // Client소켓 생성
                    // -> Accept: 실제로 대기하는 부분, Client가 접속 하면 다음 진행
                    // 이 때부터 클라이언트와 데이터 전달을 할 수 있음

                    Console.WriteLine("Client 연결");
                    // clntSocket의 값이 설정 됨을 의미

                    byte[] recvBytes = new byte[1024];
                    // 서버와 클라이언트의 버퍼로 사용 될 공간 생성
                    // 서버는 클라이언트에서 데이터를 받음
                    // -> 이 때 클라이언트는 전송을 하고 있음

                    int nRecv = clntSocket.Receive(recvBytes);
                    // 받은 데이터가 몇 바이트인지 알고 있어야 함
                    // 불필요한 데이터를 없에기 위해서 전달받은 크기를 알고 있어야 함

                    string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                    // 전송받은 데이터를 UTF-8형식으로 인코딩 한다.(한글 깨짐 방지)
                    // 받은 데이터를 0번부터 nRecv의 번째까지 인코딩

                    byte[] sendBytes = Encoding.UTF8.GetBytes("Hello: " + txt);
                    // 다시 인코딩 해서 전송 할 준비
                    clntSocket.Send(sendBytes);
                    // 만들어진 데이터를 전송
                    clntSocket.Close();
                    // 클라이언트 종료
                }
            }
        }
    }
}
