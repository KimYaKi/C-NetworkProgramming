using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace ThreadReceiveData
{
    public partial class Form1 : Form
    {
        bool Runflags = true;

        // 스레드 선언
        Thread RaceThread;

        public Form1()
        {
            InitializeComponent();

            // 스레드 초기화 & 작성한 함수를 스레드로 실행 시킬준비
            RaceThread = new Thread(new ThreadStart(ReceiveData));
        }
        private void btnReceive_Click(object sender, EventArgs e)
        {
            // 함수 호출 후 스레드로 수행
            ReceiveData();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("스레드 수행 후: {0}", i);
            }
        }

        private void ReceiveData()
        {
            while (Runflags)
            {
                Thread.Sleep(1);
                foreach (var proc in Process.GetProcesses())
                {
                    if (proc.ProcessName.ToString() == "NetworkPrograming")
                    {
                        Runflags = false;
                        this.lblReceive01.Text = "실행 됨";
                        lblReceive02.Text = ThreadSend.Form1.DataSend + "";
                    }
                }
            }
            /* Thread.Abort
             * 이 메서드가 호출
             * 되는 스레드에서 ThreadAbortException을 발생시켜 
             * 스레드 종료 프로세스를 시작합니다. 
             * 이 메서드를 호출하면 대개 스레드가 종료됩니다.
             * 출처: https://docs.microsoft.com
             */
            RaceThread.Abort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* 
             * Form을 종료 했을 때 수행중인 스레드가 있다면
             * 즉, RaceThread가 null이 아닌 수행 중 이라면
             * 스레드를 종료 시켜준다.
             * 이유: 위의 함수에서 while문을 탈출하지 못하면
             *       Abort메소드가 호출되지 않아서 스레드가 종료되지 않는다.
             *       그렇게 되서 자원을 고갈시키는 것을 방지하기 위한 작업이다.
             */
            if (RaceThread != null)
                RaceThread.Abort();
        }
    }
}
