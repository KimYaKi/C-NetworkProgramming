using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TcpCltSync
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = "localhost";
            int portNumber = 3000;

            try
            {
                IPHostEntry remote = Dns.GetHostEntry(hostName);
                IPAddress remoteIP = remote.AddressList[0];
                IPEndPoint remoteEnd = new IPEndPoint(remoteIP, portNumber);

                Socket ctSocket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
                ctSocket.Connect(remoteEnd);
                Console.WriteLine("서버와 연결됨...");
                byte[] buff = new byte[512];
                string rcvMsg = null;
                string sndMsg = "tcp sending message test .. Quit";
                byte[] sndBytes = Encoding.UTF8.GetBytes(sndMsg);
                int sndNum = ctSocket.Send(sndBytes);
                Console.WriteLine("메시지 송신완료");
                int rcvNum = ctSocket.Receive(buff);
                rcvMsg = Encoding.UTF8.GetString(buff, 0, rcvNum);
                Console.WriteLine("수신 메시지:" + rcvMsg);
                ctSocket.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
