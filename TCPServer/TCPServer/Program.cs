using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPServer
{
    class Program
    {
        static Socket sck;
        // 서버 소켓
        static Socket acc;
        // 클라이언트 통신 소켓
        static int port = 9000;
        static IPAddress ip;
        static Thread rec;
        static string name;


        // Host의 IP주소를 가져오는 과정
        static string GetIp()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }
        
        // 데이터를 전달받는 부분
        static void recV()
        {
            while (true)
            {
                Thread.Sleep(500);
                byte[] Buffer = new byte[255];
                int rec = acc.Receive(Buffer, 0, Buffer.Length, 0);
                Array.Resize(ref Buffer, rec);
                Console.WriteLine(Encoding.UTF8.GetString(Buffer));
            }
        }

        static void Main(string[] args)
        {
            rec = new Thread(recV);
            Console.WriteLine("Your Local Ip is: " + GetIp());
            Console.Write("Please enter your name");
            name = Console.ReadLine();
            Console.WriteLine("Please Enter Your Host Port");

            string inputPort = Console.ReadLine();

            // 서버의 IP와 포트를 설정하는 부분
            try {  port = Convert.ToInt32(inputPort); }
            catch { port = 9000; }
            ip = IPAddress.Parse(GetIp());

            // 설정이 완료된 IP와 Port를 기반으로 서버 소켓 생성
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(ip, port));

            sck.Listen(0);
            acc = sck.Accept();
            rec.Start();

            while (true) {
                byte[] sdata = Encoding.UTF8.GetBytes("<"+name+">"+Console.ReadLine());
                acc.Send(sdata, 0, sdata.Length, 0);
            }

        }
    }
}
