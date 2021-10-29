using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdHomeTaskTPL
{
    public class CsvData
    {
        public List<string> urls = new();

        public CsvData()
        {

        }

        public List<string> LoadString()
        {
            var csvTable = new DataTable();

            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(@"C:\IzbiraemaParallelProgrammingC#\Hometasks\ThirdHomeTaskTPL\ThirdHomeTaskTPL\File\EMag.csv")), true))
            {
                csvTable.Load(csvReader);
            }

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                urls.Add(csvTable.Rows[i][0].ToString());
            }

            return urls;
        }
    }
}
