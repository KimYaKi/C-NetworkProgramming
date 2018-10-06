using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPClient
{
    class Program
    {
        static string name = "";
        static int port = 9000;
        static IPAddress ip;
        // 서버와 통신을 연결 할 소켓
        static Socket sck;
        // 서버에서 입력한 데이터를 읽고 있을 스레드
        static Thread rec;

        static void recV()
        {
            while (true)
            {
                Thread.Sleep(500);
                byte[] Buffer = new byte[255];
                int rec = sck.Receive(Buffer, 0, Buffer.Length, 0);
                Array.Resize(ref Buffer, rec);
                Console.WriteLine(Encoding.UTF8.GetString(Buffer));
            }
        }

        static void Main(string[] args)
        {
            rec = new Thread(recV);
            Console.Write("Please enter your name : ");
            name = Console.ReadLine();
            Console.Write("Please enter the ip of the server : ");
            ip = IPAddress.Parse(Console.ReadLine());
            Console.Write("Please Enter Host Port : ");

            string inputPort = Console.ReadLine();

            // 서버의 IP와 포트를 설정하는 부분
            try { port = Convert.ToInt32(inputPort); }
            catch { port = 9000; }

            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Connect(new IPEndPoint(ip, port));
            rec.Start();

            byte[] conmsg = Encoding.UTF8.GetBytes("<" + name + ">" + "Connected");
            sck.Send(conmsg, 0, conmsg.Length, 0);

            while (sck.Connected)
            {
                byte[] sdata = Encoding.UTF8.GetBytes("<"+name+">"+Console.ReadLine());
                sck.Send(sdata, 0, sdata.Length, 0);
            }

        }
    }
}
