using System;
using System.Collections.Generic;
using System.IO;

namespace Server
{
    public class ClientChk
    {
        public ClientChk(Dictionary<string,string> List)
        {
            string[] clientList = File.ReadAllLines(@"..\..\ClientText.txt");
            foreach (string show in clientList)
            {
                string[] data = show.Split('/');
                List.Add(data[0], data[1]);
            }
        }
    }
}
