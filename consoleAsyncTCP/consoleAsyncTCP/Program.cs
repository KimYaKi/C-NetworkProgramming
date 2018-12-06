using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace consoleAsyncTCPClient
{
    class Program
    {
        public static ManualResetEvent Connect_Evnt = new ManualResetEvent(false);
        public static ManualResetEvent Sendt_Evnt = new ManualResetEvent(false);
        public static ManualResetEvent Recieve_Evnt = new ManualResetEvent(false);
        public const int bufSize = 512;
        public static byte[] rcv_buff = new byte[bufSize];
        public static string rcvMsg = null;

        public static void Connect_Cbk(IAsyncResult ar)
        {
            Thread thr = Thread.CurrentThread;
            Console.WriteLine("status of main Thread : " + thr.ThreadState);
            Socket ct = (Socket)ar.AsyncState;
            ct.EndConnect(ar);
            Connect_Evnt.Set();
        }
        public static void Send_Cbk(IAsyncResult ar)
        {
            Socket ct = (Socket)ar.AsyncState;
            int recvNum = ct.EndSend(ar);
            Sendt_Evnt.Set();

        }
        public static void Receive_Cbk(IAsyncResult ar)
        {
            Socket ct = (Socket)ar.AsyncState;
            int recvNum = ct.EndReceive(ar);
            if (recvNum > 0)
            {
                rcvMsg = Encoding.UTF8.GetString(rcv_buff, 0, recvNum);
                Console.WriteLine("서버 응답: " + rcvMsg);
                Recieve_Evnt.Set();
            }


        }
        static void Main(string[] args)
        {
            int portNum = 4000;

            //Thread thr = Thread.CurrentThread;
            //Console.WriteLine("status of main Thread : " + thr.ThreadState);
            //IPHostEntry remote = Dns.GetHostEntry(hostName);
            // IPAddress remoteIP = remote.AddressList[0];
            IPAddress remoteIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEnd = new IPEndPoint(remoteIP, portNum);
            try
            {
                Socket ctSock = new Socket(AddressFamily.InterNetwork,
                                    SocketType.Stream, ProtocolType.Tcp);
                ctSock.BeginConnect(remoteEnd, new AsyncCallback(Connect_Cbk), ctSock);
                Connect_Evnt.WaitOne();
                Console.WriteLine("client: connected ");

                string tmp = "From Client : Hello this is a test message";
                byte[] buff = Encoding.UTF8.GetBytes(tmp);
                ctSock.BeginSend(buff, 0, buff.Length, 0, new AsyncCallback(Send_Cbk), ctSock);
                Sendt_Evnt.WaitOne();
                Console.WriteLine("client: sent a message: "+ tmp); // untill here

                ctSock.BeginReceive(rcv_buff, 0, bufSize, 0, new AsyncCallback(Receive_Cbk), ctSock);
                Recieve_Evnt.WaitOne();

                ctSock.Close();
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
            }

        }
    }
}
