using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using D01tsumaTask3.Models;
using D01tsumaTask3.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace D01tsumaTask3.Controllers
{
    public class HomeController : Controller
    {
        public async ValueTask<IActionResult> Index()
        {
            const string rssEndpoint = "http://d01tsumath.hatenablog.com/rss";

            var client = new HttpClient();
            var response = await client.GetAsync(rssEndpoint);
            var rssFeed = await response.Content.ReadAsStringAsync();

            var articles
                = XElement.Parse(rssFeed)
                    .Descendants("item")
                    .Select(x =>
                    {
                        var title = x.Element("title").Value;
                        var url = x.Element("link").Value;
                        return new Article(title, url);
                    })
                    .Take(5)
                    .ToArray();

            return this.View();
        }

        /*public IActionResult Index()
        {
            string url = @"http://d01tsumath.hatenablog.com/rss";

            try
            {
                // RSS読み込み
                XElement element = XElement.Load(url);

                // channelの取得
                XElement channelElement = element.Element("channel");

                //itemの取得
                IEnumerable<XElement> elementItems = channelElement.Elements("item");

                var titleList = new List<Title>(5);

                for (int i = 0; i < 5; i++)
                {
                    XElement item = elementItems.ElementAt(i);

                    // 先頭の<link></link>を取り出す
                    var linkText = Regex.Replace(item.Element("link").ToString(), "<[^>]*?>", "");
                    var title = new Title
                    {
                        Name = item.Element("title").Value,
                        URL = linkText
                    };
                    titleList.Add(title);
                }

                ViewData["titles"] = titleList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View();
        }*/

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
