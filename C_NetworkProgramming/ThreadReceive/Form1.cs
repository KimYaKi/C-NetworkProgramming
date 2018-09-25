using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadReceive
{
    public partial class Form1 : Form
    {
        bool Runflags = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            while (Runflags)
            {
                System.Threading.Thread.Sleep(1);

                // 실행중인 프로세스를 가져온다.
                foreach (var proc in Process.GetProcesses())
                {
                    // 가져온 프로세스 명이 "NetworkPrograming"인 것이 있으면
                    if (proc.ProcessName.ToString() == "ThreadSend")
                    {
                        // while문의 반복을 종료하고 실행 됨을 알린 뒤,
                        // Label의 값을 전달 받은 데이터로 수정한다.
                        Runflags = false;
                        this.lblReceive01.Text = "실행 됨";
                        this.lblReceive02.Text = ThreadSend.Form1.DataSend + "";
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("스레드 수행 후: {0}", i);
            }
        }
    }
}
