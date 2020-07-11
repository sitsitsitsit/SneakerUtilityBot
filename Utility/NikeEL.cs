using DSharpPlus.CommandsNext;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AJAXTools.Utility
{
    public class NikeEL : BaseCommandModule
    {
        public string Region { get; set; }
        public string StyleCode { get; set; }
        public double Size { get; set; }
        public int Quantity { get; set; }

        public NikeEL(string region, string styleCode, double size, int quantity)
        {
            this.Region = region.ToLower();
            this.StyleCode = styleCode.ToLower();
            this.Size = size;
            this.Quantity = quantity;
        }
        
        public static async Task<string> DownloadHtml(string productLink)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.26.1");
            client.DefaultRequestHeaders.Add("Host", "www.nike.com");
            var response = await client.GetAsync(productLink);
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public static string AuCaLinkCreation(HtmlDocument doc, string productLink, string size)
        {
            var list = doc.DocumentNode.SelectNodes("//meta")[17];
            string productId = list.GetAttributeValue("content", "");
            string ATCLink = $"{productLink}?productId={productId}&size={size}";
            return ATCLink;
        }

        public static string ATCLinkCreation(HtmlDocument doc, string productLink, string size)
        {
            var list = doc.DocumentNode.SelectNodes("//meta")[17];
            string productId = list.GetAttributeValue("content", "");
            string ATCLink = $"{productLink}?size={size}&productId={productId}";
            return ATCLink;
        }

        public static string InitialLinkParse(HtmlDocument doc)
        {
            var title = doc.DocumentNode.SelectNodes("//h1")[0].InnerText;
            return title;
        }

        public static string ParseRegion(string productLink)
        {
            string[] splitData = productLink.Split(new string[] { "com/", "/launch" }, StringSplitOptions.None);
            return splitData[1];
        }

        public static List<string> GetSizes(string source)
        {
            var sizeRun = source.Split(new string[] { "sizeRun\":\"", ":y\",\"sizes\"" }, StringSplitOptions.None)[1];
            var sizeList = sizeRun.Split(new string[] { ":y|" }, StringSplitOptions.None);
            return sizeList.ToList();
        }

        public static string GenerateMainString(List<string> linkList, List<string> sizes, string productName)
        {
            string mainString = $"**{productName}**";
            for (int i = 0; i < sizes.Count; i++)
            {
                mainString += $"\n**Size {sizes[i]} - [Here]({linkList[i]})**";
            }
            return mainString;
        }
    }
}
