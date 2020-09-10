using NewsFeedVn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedVn.model_custom
{
    public class CategoryAndArticles
    {
        public String CategoryName { get; set; }
        public List<Article> Articles{ get; set; }
    }
}