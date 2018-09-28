using System;
using System.Windows.Forms;
using System.Threading;

namespace PrimeNumber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 각각 판별하기 위한 스레드 선언
        Thread chkNum = null;
        Thread primeNum = null;

        private void actionBtn_Click(object sender, EventArgs e)
        {
            // 홀짝 판별 스레드와 소수 판별 스레드를 시작
            chkNum = new Thread(new ParameterizedThreadStart(ChkNum));
            chkNum.Start(txtNum.Text);
            primeNum = new Thread(new ParameterizedThreadStart(PrimeNum));
            primeNum.Start(txtNum.Text);
        }

        //파라미터로 함수를 받을 수 있게 만들어 줌
        private delegate void OnResultDelegate(string strText);

        //델리게이트 객체 생성
        private OnResultDelegate ChkView = null;
        private OnResultDelegate PrimeView = null;

        // 홀,짝 판별을 위한 메소드
        private void ChkNum(object e)
        {
            long k = Convert.ToInt64(e);
            if (k % 2 == 0)
                Invoke(ChkView, k.ToString() + "는 짝수입니다.");
            else
                Invoke(ChkView, k.ToString() + "는 홀수입니다.");
            chkNum.Abort();
        }

        // 소수임을 출력 해 주기 위한 값을 정해주는 메소드
        private void PrimeNum(object e)
        {
            long k = Convert.ToInt64(e);
            if (IsPrime(k))
                Invoke(PrimeView, k.ToString() + "는(은) 소수입니다.");
            else
                Invoke(PrimeView, k.ToString() + "는(은) 소수가 아닙니다.");
            primeNum.Abort();
        }

        // 소수 판별 메소드
        public static bool IsPrime(long number)
        {
            // 음수는 포함 0,1은 판별하지 않음
            if (number <= 1) return false;
            // 2는 소수로 이미 정해짐
            if (number == 2) return true;
            // 자신을 재외한 수 2로 나누어 떨어지면 소수가 아님
            if (number % 2 == 0) return false;
            
            for (int i = 2; i < number; i++)
            {
                // 입력한 수가 자신 보다 어떤 수로든 나누어 떨어지면
                // 그 숫자는 소수가 아니게 된다.
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 델리게이트 선언 초기화
            ChkView = new OnResultDelegate(ChkNumber);
            PrimeView = new OnResultDelegate(PrimeNumber);
        }

        // 전달 받은 값들을 출력 해 주기 위한 메소드
        private void PrimeNumber(string PriNum)
        {
            this.lblPrime.Text = PriNum;
        }
        private void ChkNumber(string ChkNum)
        {
            this.lblNum.Text = ChkNum;
        }
    }
}
