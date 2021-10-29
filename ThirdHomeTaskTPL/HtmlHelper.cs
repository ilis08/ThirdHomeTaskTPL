using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThirdHomeTaskTPL
{
    public class HtmlHelper
    {
        public HtmlDocument DownloadData(string url)
        {
            Thread.Sleep(5000);
            HtmlWeb web = new();

            HtmlDocument doc = web.Load(url);

            return doc;
        }

        public List<Product> ParseData(HtmlDocument docs) 
        {
            List<Product> products = new List<Product>();

            foreach (var doc in docs.DocumentNode.SelectNodes("//a[@class='card-v2-title semibold mrg-btm-xxs js-product-url']"))
            {
                products.Add(new Product(doc.InnerHtml));
            }

            return products;
        }

    }
}
