using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP_Comm_Console
{
    class Program
    {
        static Socket serverS;
        static void serverFunc()
        {
            try
            {
                using (serverS = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    IPAddress ipAddress = IPAddress.Parse("127.0.0.1");         // 0.0.0.0은 소켓을 모든 IP에 바인딩할 때 사용 
                                                                                //IPEndPoint endPoint = new IPEndPoint(ipAddress, 10200); 
                    IPEndPoint endPoint = new IPEndPoint(ipAddress, 10200); // 위 두 줄을 IPAddress.Any를 이용하여 한 줄로 처리. 

                    serverS.Bind(endPoint);

                    byte[] recvBytes = new byte[1024];
                    EndPoint clientEP = new IPEndPoint(IPAddress.None, 0);

                    while (true)
                    {
                        int nRecv = serverS.ReceiveFrom(recvBytes, ref clientEP);
                        string str = Encoding.UTF8.GetString(recvBytes, 0, nRecv);

                        Console.WriteLine("From {0} : {1}", clientEP.ToString(), str);
                        byte[] sendBytes = Encoding.UTF8.GetBytes("Resend : " + str);
                        serverS.SendTo(sendBytes, clientEP);
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        static void Main(string[] args)
        {
            Thread th = new Thread(serverFunc);

            th.Start();

            while (true)
            {
                string line = Console.ReadLine();
                if (line == "QUIT")
                {
                    break;
                }
            }
            serverS.Close();
            th.Join();
        }
    }
}
