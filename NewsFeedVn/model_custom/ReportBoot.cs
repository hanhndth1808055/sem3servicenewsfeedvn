using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedVn.model_custom
{
    public class ReportBoot
    {
        public int TotalUrl { get; set; }
        public int Success { get; set; }
        public int Error { get; set; }
        public int NotYet { get; set; }
    }
}