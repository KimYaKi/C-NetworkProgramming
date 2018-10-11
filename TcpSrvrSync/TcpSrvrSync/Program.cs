using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TcpSrvrSync
{
    class Program
    {
        static void Main(string[] args)
        {
            string  hostName = "localhost";
            int portNUmber = 3000;

            IPHostEntry local = Dns.GetHostEntry(hostName);
            IPAddress   locIP = local.AddressList[0];
            IPEndPoint locEnd = new IPEndPoint(locIP, portNUmber);

            Socket svrSock = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream, 
                                        ProtocolType.Tcp);
            try
            {
                svrSock.Bind(locEnd);
                svrSock.Listen(5);

                while (true)
                {
                    Console.WriteLine("서버대기 ... 포트번호:" + portNUmber);
                    Socket sock2nd = svrSock.Accept();
                    string recvMsg = null;
                    byte[] buff = new byte[512];

                    while (true)
                    {
                        int recvNum = sock2nd.Receive(buff);
                        recvMsg += Encoding.UTF8.GetString(buff, 0, recvNum);
                        if (recvMsg.IndexOf("QUIT") > -1)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("수신메시지 :" + recvMsg);
                    byte[] sndmsg = Encoding.UTF8.GetBytes("FromServer:; Your Message successfully received !");
                    sock2nd.Send(sndmsg);
                    sock2nd.Close();

                }
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
