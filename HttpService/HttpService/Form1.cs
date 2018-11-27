using System;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace HttpService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            string url = textURL.Text;
            // 테스트 사이트의 주소

            if(url == "")
            {
                url = "https://httpbin.org/get";
            }
            string responseText = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초
            request.Headers.Add("Authorization", "BASIC SGVsbG8=");
            // 헤더 추가 방법

            using(HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
                Console.WriteLine(status);
                // 정상이면 OK
                textURL.Text = status.ToString();

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }
            Console.WriteLine(responseText);
            textBoxWeb.Text = responseText;
        }
    }
}
