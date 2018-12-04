using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
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
                    IPAddress ipAddress = IPAddress.Parse("210.123.254.197");         // 0.0.0.0은 소켓을 모든 IP에 바인딩할 때 사용 
                                                                                //IPEndPoint endPoint = new IPEndPoint(ipAddress, 10200); 
                    IPEndPoint endPoint = new IPEndPoint(ipAddress, 10200); // 위 두 줄을 IPAddress.Any를 이용하여 한 줄로 처리. 

                    serverS.Bind(endPoint);

                    byte[] recvBytes = new byte[1024];
                    // IP를 설정하지 않는 이유
                    // UDP통신은 특정 Client에 전송하는 것이 아니기 때문에 IP설정을 하지 않는다.
                    EndPoint clientEP = new IPEndPoint(IPAddress.None, 0);

                    while (true)
                    {
                        // ref : 값을 자동으로 채워준다.
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
