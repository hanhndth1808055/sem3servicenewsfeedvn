using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{

    public class Article
    {
        public int Id { get; set; }
        //Category
        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        [Required]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        //Source
        [ForeignKey("Source")]
        [Display(Name = "Source ID")]
        [Required]
        public int SourceId { get; set; }
        public virtual Source Source { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        [Required]
        public ArticleStatus Status { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [DefaultValue(0)]
        public int Count { get; set; }
        public enum ArticleStatus
        {
            DEACTIVE = 0, INITIAL = 1, ACTIVE = 2, DELETE = 3
        }
        public String toString()
        {
            return "ArticleId: " + Id +
                ", SourceId: " + SourceId +
                ", Title: " + Title +
                ", Content: " + Content;
        }
    }
}