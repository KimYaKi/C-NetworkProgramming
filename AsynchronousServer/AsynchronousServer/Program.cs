using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AsynchronousServer
{
    class Program
    {
        public static ManualResetEvent Accept_Evnt = new ManualResetEvent(false);
        public static ManualResetEvent Send_Evnt = new ManualResetEvent(false);
        public static ManualResetEvent Receive_Evnt = new ManualResetEvent(false);
        public const int bufSize = 512;
        public static byte[] rcv_buff = new byte[bufSize];
        public static string rcvMsg = null;
        public static Socket client = null;

        public static void Accept_Cbk(IAsyncResult ar)
        {
            Thread thr = Thread.CurrentThread;
            Console.WriteLine("status of main Thread : " + thr.ThreadState);
            Socket srvr = (Socket)ar.AsyncState;
            client = srvr.EndAccept(ar);
            Accept_Evnt.Set();
        }

        public static void Send_Cbk(IAsyncResult ar)
        {
            Socket ct = (Socket)ar.AsyncState;
            int recvNum = ct.EndSend(ar);
            Send_Evnt.Set();
        }

        public static void Receive_Cbk(IAsyncResult ar)
        {
            Socket ct = (Socket)ar.AsyncState;
            int recvNum = ct.EndReceive(ar);
            if(recvNum > 0)
            {
                rcvMsg = Encoding.UTF8.GetString(rcv_buff, 0, recvNum);
                Console.WriteLine("Client : " + rcvMsg);
                Receive_Evnt.Set();
            }
        }

        static void Main(string[] args)
        {
            int portNum = 4000;
            Thread thr = Thread.CurrentThread;
            Console.WriteLine("status of main Thread : " + thr.ThreadState);

            IPAddress locIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint locEnd = new IPEndPoint(locIP, portNum);

            try
            {
                Socket svrSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                svrSock.Bind(locEnd);
                svrSock.Listen(5);

                Console.WriteLine("Server : Start");
                svrSock.BeginAccept(new AsyncCallback(Accept_Cbk), svrSock);
                Accept_Evnt.WaitOne();
                Console.WriteLine("Server : connected a connect");

                client.BeginReceive(rcv_buff, 0, bufSize, 0, new AsyncCallback(Receive_Cbk), client);
                Receive_Evnt.WaitOne();

                byte[] buff = Encoding.UTF8.GetBytes(rcvMsg);
                client.BeginSend(buff, 0, buff.Length, 0, new AsyncCallback(Send_Cbk), client);
                Send_Evnt.WaitOne();

            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
