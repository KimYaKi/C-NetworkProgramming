using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace SMTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string mailServer = "smtp.gmail.com";
            string fromPassword = textBoxPwd.Text;

            MailAddress from = new MailAddress(textBoxFrom.Text);
            MailAddress to = new MailAddress(textBoxTo.Text);

            // 메일 서버에 특정 포트 번호로 메일 전송을 요청 하는 부분
            SmtpClient client = new SmtpClient(mailServer, 587);

            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // 보내는 사람의 정보를 함께 전송한다.(from.Address, fromPassword);
            client.Credentials = new NetworkCredential(from.Address, fromPassword);
            client.Timeout = 20000;

            MailMessage message = new MailMessage(from, to)
            {
                Subject = textBoxTitle.Text,
                SubjectEncoding = System.Text.Encoding.UTF8,
                Body = textBoxBody.Text,
                BodyEncoding = System.Text.Encoding.UTF8
            };
            try
            {
                client.Send(message);
                MessageBox.Show("메시지 전송 성공!!");
                textBoxBody.Clear();
                textBoxTitle.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                message.Dispose();
            }
        }
    }
}
