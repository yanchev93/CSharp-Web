using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
        const string NEW_LINE = "\r\n";

        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 8080);
            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();

                await ProcessClientAsync(client);
            }
        }

        public static async Task ProcessClientAsync(TcpClient client)
        {
            var stream = client.GetStream();

            byte[] buffer = new byte[1_000_000];
            int byteLength = 0;

            var length = await stream.ReadAsync(buffer, byteLength, buffer.Length);

            var requestedString = Encoding.UTF8.GetString(buffer, byteLength, length);
            Console.WriteLine(requestedString);


            bool sessionSet = false;
            if (requestedString.Contains("sid="))
            {
                sessionSet = true;
            }

            string html = $"<h1> Hello World from Teo. <br>" +
                $"I'm currently in Portalnd, ME, USA and the time here is {DateTime.Now.ToString("dd/mm/yy - hh:mm tt")} </h1>";

            string response = "HTTP/1.1 200 OK" + NEW_LINE +
                "Server: MyFirstWebServer 2021" + NEW_LINE +
                //"Location: https://www.google.com" + NEW_LINE +
                "Content-Type: text/html; charset=utf-8" + NEW_LINE + // NB!! Very Important for the browser!! NB!!
                "Set-Cookie: sid=7812asd3918238asdag79123" + DateTime.UtcNow.AddHours(2).ToString("R") + NEW_LINE +
                "Content-Length: " + html.Length + NEW_LINE +
                NEW_LINE + html + NEW_LINE;

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseBytes);

            stream.Close();
            Console.WriteLine(new string('=', 50));

        }

        public static async Task ReadData()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string url = "https://softuni.bg/";
            HttpClient httpClient = new HttpClient();
            var respones = await httpClient.GetAsync(url);

            Console.WriteLine(respones.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine, respones.Headers.Select(x => x.Key + ": " + x.Value)));

            // var html = await httpClient.GetStringAsync(url);
            // Console.WriteLine(html);
        }
    }
}
