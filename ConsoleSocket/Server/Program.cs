using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            // IPAddress.Any == IPAddress.Parse("0.0.0.0") == 0 -> 동일한 의미

            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sck.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1994));
            sck.Listen(0);

            Socket acc = sck.Accept();
            // 연결된 클라이언트와 통신을 수행 할 소켓 생성

            byte[] buffer = Encoding.Default.GetBytes("Hello World!");
            acc.Send(buffer, 0, buffer.Length, SocketFlags.None);
            // SocketFlags.None == 0 을 의미

            buffer = new byte[255];

            int rec = acc.Receive(buffer, 0, buffer.Length, 0);
            // 전달 받은 데이터의 크기

            Array.Resize(ref buffer, rec);

            Console.WriteLine("Received: {0}", Encoding.Default.GetString(buffer));

            sck.Close();
            acc.Close();

            Console.Read();

        }
    }
}
