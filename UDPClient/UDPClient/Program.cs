using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    class Program
    {
        static IPAddress GetCurrentIPAddress()
        {
            IPAddress[] addrs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            foreach (IPAddress ipAddr in addrs)
            {
                if (ipAddr.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddr;
                }
            }

            return null;
        }

        static void clientFunc()
        {
            Socket clientS = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //EndPoint serverEP = new IPEndPoint(GetCurrentIPAddress(), 10200);
            // 서버의 주소와 서버의 포트 번호를 알고 있어야 함
            EndPoint serverEP = new IPEndPoint(IPAddress.Parse("210.123.254.97"), 10200);
            // 비어있는 EndPoint를 생성
            EndPoint senderEP = new IPEndPoint(IPAddress.None, 0);

            // 값을 전송하기 위해 Encoding과정 후 전송
            byte[] buf = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            clientS.SendTo(buf, serverEP);

            byte[] recvBuf = new byte[1024];
            // 상대방이 정해져 있지 않은 상태로 값을 전송
            int nRecv = clientS.ReceiveFrom(recvBuf, ref senderEP);

            string str = Encoding.UTF8.GetString(recvBuf, 0, nRecv);

            Console.WriteLine(str);

            while (true)
            {
                str = Console.ReadLine();
                if (str.Equals("quit")) break;
                buf = Encoding.UTF8.GetBytes(str);
                clientS.SendTo(buf, serverEP);
                nRecv = clientS.ReceiveFrom(recvBuf, ref senderEP);
                str = Encoding.UTF8.GetString(recvBuf, 0, nRecv);
                Console.WriteLine(str);
            }

            clientS.Close();
        }
        static void Main(string[] args)
        {
            clientFunc();
        }
    }
}
