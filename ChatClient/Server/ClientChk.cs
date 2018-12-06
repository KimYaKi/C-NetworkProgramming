using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientChk
    {
        public ClientChk(Dictionary<string, string> List)
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
