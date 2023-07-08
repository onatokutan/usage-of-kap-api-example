using System;
using System.Net;
using Spire.Pdf;
 
namespace kapBildirim
{
    public partial class kap
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            var kapText = client.DownloadString(@"https://www.kap.org.tr/tr/api/disclosures");
            dynamic kapList = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(kapText);
            string temp = null;
            PdfDocument doc = new PdfDocument();

            foreach (var i in kapList)
            {
                temp = i["basic"]["disclosureIndex"].ToString();
                Console.WriteLine(temp);
                temp = i["basic"]["title"].ToString();
                Console.WriteLine(temp);  
                temp = i["basic"]["summary"].ToString();
                Console.WriteLine(temp); 
                temp = i["basic"]["companyName"].ToString();
                Console.WriteLine(temp); 
                using (MemoryStream ms = new MemoryStream(client.DownloadData("https://www.kap.org.tr/tr/BildirimPdf/"+i["basic"]["disclosureIndex"])))
                {
                doc.LoadFromStream(ms);

                }
                doc.SaveToFile(temp+".pdf", FileFormat.PDF);

            }

        }

    }

}