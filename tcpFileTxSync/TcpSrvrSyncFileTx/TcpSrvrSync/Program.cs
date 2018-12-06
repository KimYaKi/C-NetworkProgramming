using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpSrvrSync
{
    class Program
    {
        static void Main(string[] args)
        {

            int portNumber = 3000;

            IPAddress locIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint locEnd = new IPEndPoint(locIP, portNumber);

            Socket svrSock = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream, 
                                        ProtocolType.Tcp);
            try
            {
                svrSock.Bind(locEnd);
                svrSock.Listen(5);

                while (true)
                {
                    Console.WriteLine("서버대기 ... 포트번호:" + portNumber);
                    Socket client = svrSock.Accept();

                   
                    byte[] buff = new byte[512];

                    string output_PATH = @"FileDown";
                    int BytesPerRead = 1024;
                    if (!Directory.Exists(output_PATH))
                        Directory.CreateDirectory(output_PATH);

                    byte[] filenameByte = new byte[256];
                    int fsize = client.Receive(filenameByte, 0, 256, SocketFlags.None);

                    string filename = Encoding.UTF8.GetString(filenameByte,0,fsize);
                    string[] tokens = filename.Split(':');

                    if (tokens[0].Equals("FILE")) // file tx mode 
                    {
                        int.TryParse(tokens[1], out int fileLenth);
                        int.TryParse(tokens[2], out int fileNameLength);
                        string fileName = tokens[3];

                        Console.WriteLine(fileName);
                        Console.ReadLine();

                        FileStream fs = File.Create(output_PATH + fileName);
                        byte[] buffer = new byte[BytesPerRead];

                        int bytesRead;

                        // 이부분이 중요햄
                        while ((bytesRead = client.Receive(buffer, 0, buffer.Length, SocketFlags.None)) > 0)
                        {
                            fs.Write(buffer, 0, bytesRead);
                            Console.WriteLine(bytesRead);
                        }
                        fs.Close();

                    }




                    client.Disconnect(false);
                    client.Close();

                }
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();
        }
    }
}
