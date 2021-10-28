using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ThirdHomeTaskTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvData csv = new CsvData();

            HtmlHelper helper = new HtmlHelper();

            List<Product> products = new();

            for (int i = 0; i < csv.urls.Count; i++)
            {
                Task<HtmlDocument> task1 = new(() => helper.DownloadData(csv.urls[i]));
                Task<List<Product>> task2 = task1.ContinueWith(p => helper.ParseData(p.Result));

                task1.Start();

                task2.Wait();

                products.AddRange(task2.Result);
            }

            foreach (var item in products)
            {
                Console.WriteLine(item.Name);
            }
        }

        protected static string DataLoad(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.GetStringAsync(fullUrl);

            return response.Result;
        }
    }
}
