using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AsynchronousClient
{
    class Program
    {
        public static ManualResetEvent Connect_Evnt = new ManualResetEvent(false);
        public static ManualResetEvent Send_Evnt = new ManualResetEvent(false);
        public static ManualResetEvent Receive_Evnt = new ManualResetEvent(false);
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
            Send_Evnt.Set();
        }

        public static void Receive_Cbk(IAsyncResult ar)
        {
            Socket ct = (Socket)ar.AsyncState;
            int recvNum = ct.EndReceive(ar);
            if(recvNum > 0)
            {
                rcvMsg = Encoding.UTF8.GetString(rcv_buff, 0, recvNum);
                Console.WriteLine("서버 응답 : " + rcvMsg);
                Receive_Evnt.Set();
            }
        }

        static void Main(string[] args)
        {
            int portNum = 4000;
            IPAddress remoteIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEnd = new IPEndPoint(remoteIP, portNum);
            try {
                Socket ctSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ctSock.BeginConnect(remoteEnd, new AsyncCallback(Connect_Cbk), ctSock);
                Connect_Evnt.WaitOne();
                Console.WriteLine("client : connected");

                string tmp = "From Client : Hello this is a test message";
                byte[] buff = Encoding.UTF8.GetBytes(tmp);
                ctSock.BeginSend(buff, 0, buff.Length, 0, new AsyncCallback(Send_Cbk), ctSock);
                Send_Evnt.WaitOne();
                Console.WriteLine("client : send a message : " + tmp);

                ctSock.BeginReceive(rcv_buff, 0, bufSize, 0, new AsyncCallback(Receive_Cbk), ctSock);
                Receive_Evnt.WaitOne();
                ctSock.Close();

            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
