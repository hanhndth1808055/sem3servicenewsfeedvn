using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class Source
    {
        public int Id { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string Path { get; set; }
        //Category
        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
        [Display(Name = "Link Selector")]
        [Required]
        public string LinkSelector { get; set; }
        [Display(Name = "Title Selector")]
        [Required]
        public string TitleSelector { get; set; }
        [Display(Name = "Description Selector")]
        [Required]
        public string DescriptionSelector { get; set; }
        [Display(Name = "Content Selector")]
        [Required]
        public string ContentSelector { get; set; }
        [Display(Name = "Removal Selector")]
        [Required]
        public string RemovalSelector { get; set; }
        public SourceStatus Status { get; set; }
        public enum SourceStatus
        {
            DEACTIVE = 0, ACTIVE = 1, DELETE = 2

        }
        public String toString()
        {
            return "SourceID: " + Id +
                    ", Domain: " + Domain +
                    ", Path: " + Path +
                    ", CategoryID: " + CategoryID +
                    ", LinkSelector: " + LinkSelector +
                    ", TitleSelector: " + TitleSelector +
                    ", DescriptionSelector: " + DescriptionSelector +
                    ", ContentSelector: " + ContentSelector +
                    ", RemovalSelector: " + RemovalSelector +
                    ", Status: " + Status.ToString();
        }
    }
}