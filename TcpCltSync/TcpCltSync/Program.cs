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
            // string hostName = "localhost";
            int portNumber = 3000;

            try
            {
                // IPHostEntry remote = Dns.GetHostEntry(hostName);
                IPAddress remoteIP = IPAddress.Parse("210.123.254.197");

                IPAddress adAddr2 = new IPAddress(new byte[] { 210, 123, 254, 197 });
                Console.WriteLine(adAddr2);
                IPEndPoint remoteEnd = new IPEndPoint(remoteIP, portNumber);

                Socket ctSocket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);

                ctSocket.Connect(remoteEnd);
                // 서버의 Accept부분과 매칭

                Console.WriteLine("서버와 연결됨...");
                // 연결이 완료된 경우 출력

                byte[] buff = new byte[512];
                string rcvMsg = null;
                string sndMsg = "tcp sending message test .. \na b c d e ... \nQuit";
                byte[] sndBytes = Encoding.UTF8.GetBytes(sndMsg);
                // 전송 데이터 인코딩
                int sndNum = ctSocket.Send(sndBytes);
                // 서버에서 Receive에 해당되는 부분

                Console.WriteLine("메시지 송신완료");
                // Send가 완료되면 출력

                int rcvNum = ctSocket.Receive(buff);
                // 서버에서 Send를 하는 부분과 매칭
                rcvMsg = Encoding.UTF8.GetString(buff, 0, rcvNum);
                // 서버에서 전송 받은 데이터를 인코딩
                Console.WriteLine("수신 메시지:" + rcvMsg);
                // 서버에서 전송 받은 데이터 툴력
                ctSocket.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                // 에러가 났을 경우 그에 해당하는 이유를 출력 해줌
            }
        }
    }
}
