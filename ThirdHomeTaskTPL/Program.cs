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

            List<Product> products = new List<Product>();
           
            Task<List<string>> task1 = new(() => csv.LoadString());

            task1.Start();

            task1.Wait();

            List<Task<List<Product>>> taskList = new();

            foreach (var item in task1.Result)
            {
                taskList.Add(Task.Run(() =>
                {
                    return ProcessData(item);
                }));
            }

            var tasks = Task.WhenAll(taskList);

            var final = tasks.ContinueWith((tasks) =>
            {
                foreach (var task in tasks.Result)
                {
                    foreach (var item in task)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
            });

            final.Wait();
           
        }

        protected static List<Product> ProcessData(string url)
        {
            HtmlHelper helper = new HtmlHelper();

            Task<HtmlDocument> task1 = new(() => helper.DownloadData(url));

            task1.Start();

            Task<List<Product>> task2 = task1.ContinueWith(c => helper.ParseData(c.Result));

            return task2.Result;
        }
    }
}
