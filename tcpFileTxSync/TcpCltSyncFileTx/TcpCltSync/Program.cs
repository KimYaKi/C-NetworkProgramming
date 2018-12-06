using System;
using System.IO;
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

            int portNumber = 3000;

            try
            {
                IPAddress remoteIP = IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEnd = new IPEndPoint(remoteIP, portNumber);

           
                Socket client = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
                
                Console.WriteLine(@"서버와 연결됨...  c:\test.png");
                // file 이름 읽기
                string fileString = Console.ReadLine();
                FileInfo fileInfo = new FileInfo("C:\\Users\\rlawl\\Source\\Repos\\C-NetworkProgramming\\tcpFileTxSync\\TcpCltSyncFileTx\\test.png");
                string fileLength = fileInfo.Length.ToString();
                string fileName = fileInfo.Name;
                string fileNameLength = fileName.Length.ToString();
                byte[] fileInfoByte = Encoding.UTF8.GetBytes(
                    "FILE:" + fileLength + ":" + fileNameLength + ":" + fileName + ":");
                Console.WriteLine(Encoding.UTF8.GetString(fileInfoByte));

                client.Connect(remoteEnd);

                if (File.Exists(fileString))  // file 존재 확인
                {
                    client.Send(fileInfoByte);
                    client.SendFile(fileString);  // 파일 전송

                }
                Console.WriteLine("file is sent:");



                //byte[] buff = new byte[512];
                //string rcvMsg = null;
                //string sndMsg = "tcp sending message test .. Quit";
                //byte[] sndBytes = Encoding.UTF8.GetBytes(sndMsg);
                //int sndNum = client.Send(sndBytes);
                //Console.WriteLine("메시지 송신완료");
                //int rcvNum = client.Receive(buff);
                //rcvMsg = Encoding.UTF8.GetString(buff, 0, rcvNum);
                //Console.WriteLine("수신 메시지:" + rcvMsg);

                client.Disconnect(false);
                client.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();
        }
    }
}
