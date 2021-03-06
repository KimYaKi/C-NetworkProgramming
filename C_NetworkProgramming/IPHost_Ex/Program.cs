﻿using System;
using System.Net;

namespace IPHost_Ex
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------------------------------------------------------

            string host_name = "www.google.co.kr";
            // 호스트의 도메인 네임 선언
            IPHostEntry entry = Dns.GetHostEntry(host_name);
            // 해당 도메인에 할당된 주소들의 집합을 모두 받아옴
            // IPHostEntry ip_address = Dns.GetHostByAddress(IPAddress.Parse("172.217.27.67"));
            // IP주소를 가지고 도메인 네임을 추적

            foreach(IPAddress iPAddress in entry.AddressList)
            {
                // IP주소 출력
                Console.WriteLine(iPAddress);
            }
            string myComputer = Dns.GetHostName();
            Console.WriteLine("컴퓨터 이름: " + myComputer);

            // ---------------------------------- 수업시간에 한 내용 ------------------------------
            Console.WriteLine("----------- 과제 -----------");

            IPAddress ipaddr = IPAddress.Parse("172.217.27.67");

            // GetHostByAddress
            IPHostEntry hostEntry = Dns.GetHostByAddress(ipaddr);
            Console.WriteLine("Host Name : "+hostEntry.HostName);
            foreach (IPAddress ip in hostEntry.AddressList)
                Console.WriteLine("IP Address : "+ip);

            // GetHostByName
            hostEntry = Dns.GetHostByName("www.google.co.kr");
            Console.WriteLine("Host Name : " + hostEntry.HostName);
            foreach (IPAddress ip in hostEntry.AddressList)
            {
                Console.WriteLine("IP Address : " + ip);
            }
        }
    }
}
