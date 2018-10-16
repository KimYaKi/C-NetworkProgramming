using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TcpSrvrSync
{
    class Program
    {
        static void Main(string[] args)
        {
            string  hostName = "localhost";
            int portNUmber = 3000;

            IPHostEntry local = Dns.GetHostEntry(hostName);
            IPAddress   locIP = IPAddress.Parse("210.123.254.197");
            // IPAddress   locIP = local.AddressList[0];
            // VMWare가 작동하기 때문에 그 다음의 IP주소를 찾아서 입력해야 한다.
            // 출력결과 '::1' 로 출력됨

            IPEndPoint locEnd = new IPEndPoint(locIP, portNUmber);
            // IP주소와 포트 번호를 하나로 묶어서 객체로 생성
            Console.WriteLine(locEnd.ToString());

            //소켓 생성
            Socket svrSock = new Socket(
                AddressFamily.InterNetwork,  // IP주소 체계
                SocketType.Stream,           // TCP / UDP 설정
                ProtocolType.Tcp             // 프로토콜 설정
                );
            try
            {
                // 서버 소켓은 항상 클라이언트의 연결 요청을 받을 준비를 하고 있어야 한다.
                // 클라이언트의 요청이 수락 되면 해당 클라이언트에 대한 전용 소켓을 하나 생성한다.

                svrSock.Bind(locEnd);   // 에러 발생 부분
                // 소켓 설정과 IP주소, Port번호를 하나의 객체로 생성
                svrSock.Listen(5);
                // 클라이언트 요청 대기

                while (true)
                {
                    Console.WriteLine("서버대기 ... 포트번호:" + portNUmber);
                    Socket sock2nd = svrSock.Accept();
                    // 클라이언트의 요청이 들어 올 때 까지 대기상태
                    // 클라이언트 요청 수락 시 클라이언트에 대한 소켓 생성
                    // 클라이언트와 실제로 데이터를 주고 받을 때 사용하는 소켓

                    string recvMsg = null;
                    // 이 줄로 내려 왔을 경우 클라이언트의 요청이 수락 됨을 의미한다.
                    // 클라이언트에서는 Connect하는 부분에 해당됨
                    // 서버는 클라이언트를 식별할 수 있다.

                    byte[] buff = new byte[512];
                    // byte배열로 문자를 주고 받을 버퍼 생성

                    while (true)
                    {
                        int recvNum = sock2nd.Receive(buff);
                        // 클라이언트에게 받은 데이터의 길이
                        // 클라이언트에서는 Send에 해당되는 부분

                        recvMsg += Encoding.UTF8.GetString(buff, 0, recvNum);
                        // 문자열 인코딩 byte, 시작번호, 길이

                        // 받은 문자열에 "QUIT"이 있을 경우 while문을 종료
                        if (recvMsg.IndexOf("Quit") > -1)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("수신메시지 :" + recvMsg);
                    byte[] sndmsg = Encoding.UTF8.GetBytes("FromServer:; Your Message successfully received !");
                    // 클라이언트에 전송할 데이터 생성
                    sock2nd.Send(sndmsg);
                    // 클라이언트에 데이터 전송
                    sock2nd.Close();
                    // 클라이언트에 대한 소켓 연결 종료

                }
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
