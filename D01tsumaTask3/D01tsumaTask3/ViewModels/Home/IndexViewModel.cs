using System;
using System.Collections.Generic;
using D01tsumaTask3.Models;

namespace D01tsumaTask3.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Article> Articles { get; }

        public IndexViewModel(IEnumerable<Article> articles)
            => this.Articles = articles ?? throw new ArgumentNullException(nameof(articles));
    }
}