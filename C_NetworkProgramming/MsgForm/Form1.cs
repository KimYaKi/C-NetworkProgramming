using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Timers;

namespace MsgForm
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer TimerEvent;
        // Timer 객체 생성

        public Form1()
        {
            InitializeComponent();
        }

        private delegate void OnDelegateHeight(int Flag);
        private OnDelegateHeight OnHeight = null;

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Form Close
        }

        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.picClose.Image = Image.FromFile("");
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            this.picClose.Image = Image.FromFile("");
        }

        private void picClose_MouseMove(object sender, MouseEventArgs e)
        {
            this.picClose.Image = Image.FromFile("");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnHeight = new OnDelegateHeight(MsgView);
            this.Size = new Size(170, 0);
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width
                - this.Width - 20, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            TimerEvent = new System.Timers.Timer(2);
            TimerEvent.Elapsed += new ElapsedEventHandler(OnPopUp);
            TimerEvent.Start();
        }

        private void MsgView(int Flag)
        {
            if (Flag == 0)
            {
                Height++;
                Top--;
            }
            else if (Flag == 1)
            {
                Height--;
                Top++;
            }
            else if (Flag == 2)
            {
                this.Close();
            }
        }
        private void OnPopUp(object sender, ElapsedEventArgs e)
        {
            if(Height < 120)
            {
                Invoke(OnHeight, 0);
            }
            else
            {
                TimerEvent.Stop();
                TimerEvent.Elapsed -= new ElapsedEventHandler(OnPopUp);

                TimerEvent.Elapsed += new ElapsedEventHandler(OnPopOut);
                TimerEvent.Interval = 3000;
                TimerEvent.Start();
            }
            Application.DoEvents();
        }

        private void OnPopOut(object sender, ElapsedEventArgs e)
        {
            while(Height > 2)
            {
                Invoke(OnHeight, 1);
            }
            TimerEvent.Stop();
            Application.DoEvents();
            Invoke(OnHeight, 2);
        }
    }
}
