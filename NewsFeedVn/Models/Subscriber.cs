using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeedVn.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        //USER
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}