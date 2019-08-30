using System;
using System.Collections.Generic;

namespace D01tsumaTask3.Models
{
    public class Article
    {
        public string Title { get; }
        public string Url { get; }

        public Article(string title, string url)
        {
            this.Title = title;
            this.Url = url;
        }
    }

    /*public class Title
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }*/
}
