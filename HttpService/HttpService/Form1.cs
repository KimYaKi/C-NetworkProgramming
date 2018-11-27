using System;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

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
            // 요청을 보내는 HttpWebRequest 클래스
            // 해당 클래스 상위의 WebRequest를 사용
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

        private void buttonPost_Click(object sender, EventArgs e)
        {
            string data = "{ \"id\": \"" + textBoxID.Text + "\", \"name\" : \"" + textBoxName.Text + "\" }";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://httpbin.org/post");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = 30 * 1000;
            // request.Headers.Add("Authorization", "BASIC SGVsbG8=");

            // POST할 데이터를 Request Stream에 쓴다
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            request.ContentLength = bytes.Length;
            // 전송 할 바이트 수 지정

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bytes, 0, bytes.Length);
            }

            // Response 처리
            string responseText = string.Empty;
            using(WebResponse resp = request.GetResponse())
            {
                Stream respStream = resp.GetResponseStream();
                using(StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }

            Console.WriteLine(responseText);
            textBoxPost.Text = responseText;
        }

        private void buttonImageDown_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://httpbin.org/image/png");
            request.Method = "GET";

            // Response 바이너리 데이터 처리. 이미지 파일로 저장
            using(WebResponse resp = request.GetResponse())
            {
                var buff = new byte[1024];
                int pos = 0;
                int count;
                string filename = "image.png";
                using(Stream stream = resp.GetResponseStream())
                {
                    using(var fs = new FileStream(filename, FileMode.Create))
                    {
                        do
                        {
                            count = stream.Read(buff, pos, buff.Length);
                            fs.Write(buff, 0, count);
                        } while (count > 0);
                    }
                }
                imagePB.Image = new Bitmap(filename);
            }
        }
    }
}
