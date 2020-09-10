using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }
        //Article
        [ForeignKey("Article")]
        [Display(Name = "Article ID")]
        [Required]
        public int ArticleID { get; set; }
        public virtual Article Article { get; set; }
        //USER
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        [Required]
        public string UserID { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}