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

            // 요청을 보내는 HttpWebRequest 클래스
            // 해당 클래스 상위의 WebRequest를 사용
            // Web Request를 만드는 부분
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            // Request Method를 GET으로 설정
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초
            // 헤더 추가 방법
            request.Headers.Add("Authorization", "BASIC SGVsbG8=");

            // 본인이 만든 Request 대한 Response를 받겠다는 부분
            using(HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;
                // 정상이면 OK
                Console.WriteLine(status);
                textURL.Text = status.ToString();

                // Status Code를 보고 HTML문서를 출력
                // 여러 형태를 동일한 형태로 설정하는 Stream을 사용
                Stream respStream = resp.GetResponseStream();
                // Stream을 StreamReader를 이용하여 읽기 모드로 출력
                // 쓰기모드는 StreamWriter
                using (StreamReader sr = new StreamReader(respStream))
                {
                    responseText = sr.ReadToEnd();
                }
            }
            Console.WriteLine(responseText);
            textBoxWeb.Text = responseText;
        }

        // Client에서 Server로 Data를 전송할 때에는 POST를 사용
        private void buttonPost_Click(object sender, EventArgs e)
        {
            // POST를 전송하기 위한 형태
            string data = "{ \"id\": \"" + textBoxID.Text + "\", \"name\" : \"" + textBoxName.Text + "\" }";

            // Request를 생성
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://httpbin.org/post");
            // Method는 POST
            request.Method = "POST";
            // ContentType은 JSON형식
            request.ContentType = "application/json";
            request.Timeout = 30 * 1000;
            // request.Headers.Add("Authorization", "BASIC SGVsbG8=");

            // POST할 데이터를 Request Stream에 쓴다
            // Encoding형식은 영어만 입력 했기 때문에 ASCII형식으로 변환
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            // 전송 할 바이트 수 지정
            request.ContentLength = bytes.Length;

            // Header만 보내는 것이 아닌 Data를 보내야 하기 때문에 값을 생성해서 전송 해야 함
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

        // 그림에 대한 Request 메소드
        private void buttonImageDown_Click(object sender, EventArgs e)
        {
            // Image에 대한 file을 요청
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://httpbin.org/image/png");
            // Method는 GET 방식
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
