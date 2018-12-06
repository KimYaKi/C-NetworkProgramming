using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ListForm
{
    public partial class lsForm : Form
    {
        Socket mainSocket;
        public lsForm(string id, Socket recvSocket)
        {
            InitializeComponent();
            string list_code = "400";
            mainSocket = recvSocket;
            Console.WriteLine(list_code);
            Console.WriteLine(id);
            Console.WriteLine(mainSocket.RemoteEndPoint.ToString());
        }
    }
}
