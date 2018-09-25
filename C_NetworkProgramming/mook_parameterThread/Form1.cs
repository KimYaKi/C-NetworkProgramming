using System;
using System.Windows.Forms;
using System.Threading;

namespace mook_parameterThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread SumThread = null;

        private void btnSum_Click(object sender, EventArgs e)
        {
            SumThread = new Thread(new ParameterizedThreadStart(NumSum));
            SumThread.Start(this.txtNum.Text);
        }

        private void NumSum(object n)
        {
            long sum = 0;
            long k = Convert.ToInt64(n);
            for (long i = 0; i <= k; i++)
            {
                Thread.Sleep(1);
                sum += i;
                /*
                 * 메인 스레드가 함께 사용을 하기 때문에 일반적인
                 * 스레드 사용과 동일하게 사용하면 에러가 남
                 */
                Invoke(ResultView, "계산중 : " + sum.ToString());
                // 델리게이트 메소드를 호출 해 준다.
            }
            Invoke(ResultView, "완료 결과 : " + sum.ToString());
            SumThread.Abort();
        }

        private delegate void OnResultDelegate(string strText);
        //파라미터로 함수를 받을 수 있게 만들어 줌
        private OnResultDelegate ResultView = null;
        //델리게이트 객체 생성

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (SumThread != null)
                SumThread.Abort();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            ResultView = new OnResultDelegate(ResultSum);
        }

        private void ResultSum(string NumSum)
        {
            this.lblResult.Text = NumSum;
        }
    }
}
