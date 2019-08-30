using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D01tsumaTask3.Models;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace D01tsumaTask3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
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
        }

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
