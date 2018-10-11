using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread clientThread = new Thread(clientFunc);
            clientThread.IsBackground = true;
            clientThread.Start();

            Console.WriteLine("종료하려면 아무 키나 누르세요...");
            Console.ReadLine();
        }

        private static void clientFunc(object obj)
        {
            Socket socket = null;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 서버와 동일한 형태의 소켓을 생성 해야 한다.

            EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 11200);
            // Client에서 IPEndPoint에 담긴 정보는 서버의 정보이다.
            socket.Connect(serverEP);
            // 위에서 선언 한 정보를 가지고 서버에 접속한다.
            /* 클라이언트의 주소와 포트는 데이터를 전송 할 때 
             * 운영체제에서 새롭게 만들어서 전송하기 때문에 선언 할 필요가 없다. */

            byte[] buf = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            socket.Send(buf);

            byte[] recvBytes = new byte[1024];
            // 위의 정보는 서버의 정보와 맞춰주는 것이 좋다.
            int nRecv = socket.Receive(recvBytes);
            // 받은 데이터의 량

            string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
            Console.WriteLine(txt);

            if (socket != null)
                socket.Close();

            Console.WriteLine("TCP Client socket: Closed");
        }
        /*
         * 1. 소켓 생성 (본인의 정보는 굳이 선언하지 않는다.)
         * 2. Connect
         */
    }
}