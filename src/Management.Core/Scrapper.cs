using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Management.Core
{
    public class Scrapper
    {
        private const string _baseUrl = "https://www.wired.com";
        private WebClient _client;
        public Scrapper()
        {
        }
        public HtmlDocument GetHtmlDocument(string url)
        {
            _client = new WebClient { Encoding = Encoding.UTF8 };
            var html = _client.DownloadString(url);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }

        public List<Dictionary<string, string>> GetTitles()
        {
            var document = GetHtmlDocument(_baseUrl);
            const string selectedHtml = @"//ul[@class='post-listing-component__list']";
            var ulList = document.DocumentNode.SelectNodes(selectedHtml)[1];
            var liList = ulList.SelectNodes("li");
            List<Dictionary<string, string>> dictList = new List<Dictionary<string, string>>();

            foreach (var item in liList)
            {
                var title = item.SelectSingleNode("a/div/h5").InnerText;
                var url = item.SelectSingleNode("a").Attributes["href"].Value;
                var description = GetDetail(url);
                dictList.Add(new Dictionary<string, string>
                {
                    {"title", title},
                    {"url", url  },
                    {"desc", description }
                });
            }

            return dictList;
        }

        public string GetDetail(string url)
        {
            var document = GetHtmlDocument($"{_baseUrl}{url}");

            const string selectedHtml = @"//div[@class='grid--item body body__container article__body grid-layout__content']";
            var divList = document.DocumentNode.SelectNodes(selectedHtml);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in divList)
            {
                var newsDetail = item.InnerText;
                stringBuilder.AppendLine(newsDetail);
            }
            return stringBuilder.ToString();
        }
    }
}
