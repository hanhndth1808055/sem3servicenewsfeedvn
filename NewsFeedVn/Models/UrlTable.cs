using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class UrlTable
    {

        public int Id { get; set; }
        [Required]
        public String url{ get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }

    }
}