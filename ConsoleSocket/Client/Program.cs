using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1994);
            // 연결을 요청할 서버의 정보
            sck.Connect(endPoint);
            // 연결 요청 시작

            while (true)
            {
                Console.WriteLine("Enter Message: ");
                string msg = Console.ReadLine();
                byte[] msgBuffer = Encoding.Default.GetBytes(msg);
                sck.Send(msgBuffer, 0, msgBuffer.Length, 0);

                byte[] buffer = new byte[255];
                int rec = sck.Receive(buffer, 0, buffer.Length, 0);

                Array.Resize(ref buffer, rec);

                Console.WriteLine("Received: {0}", Encoding.Default.GetString(buffer));
                Console.Read();
            }
            // FirstBranch Commit Test
        }
    }
}
