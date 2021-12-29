using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ReadData();
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
