using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeedVn.Models
{
    public class QuestionAndAnswer
    {
        public int Id { get; set; }
        //USER
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        //Category
        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        [Required]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}