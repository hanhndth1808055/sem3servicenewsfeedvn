using NewsFeedVn.model_custom;
using NewsFeedVn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NewsFeedVn.service
{
    public class DataService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Article> GetArticlesBySourceId(int SourceId)
        {
            Debug.WriteLine("Start get articles by sourceId: " + SourceId);
            List<Article> articles = db.Articles
                   .SqlQuery("Select * from Articles where sourceId= " + SourceId+ " and status = 2")
                   .ToList<Article>();
            return articles;
        }
        public List<Article> GetArticlesByCategoryId(int CategoryId)
        {
            Debug.WriteLine("Start get articles by CategoryId: " + CategoryId);
            List<Article> articles = db.Articles
                   .SqlQuery("Select * from Articles where CategoryID = " + CategoryId + " and status = 2")
                   .ToList<Article>();
            return articles;
        }
        public List<CategoryAndArticles> GetArticlesByCategory()
        {
            //lấy 10 articles của mỗi category
            Debug.WriteLine("Start get articles by Category");
            List<CategoryAndArticles> result = new List<CategoryAndArticles>();
            List<Category> Categories = db.Categories
                   .SqlQuery("Select * from Categories")
                   .ToList<Category>();
            foreach(Category category in Categories)
            {
                List<Article> articles = db.Articles
                   .SqlQuery("Select top 10 * from Articles where CategoryID = " + category.Id + " and status = 2 ")
                   .ToList<Article>();
                CategoryAndArticles data = new CategoryAndArticles()
                {
                    CategoryName = category.Name,
                    Articles = articles
                };
                result.Add(data);
            }
            return result;
        }
        public ReportBoot ReportBoot(String Start, String End)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime StartDate = DateTime.ParseExact(Start,"yyyy-MM-dd", null);
            DateTime EndDate = DateTime.ParseExact(End, "yyyy-MM-dd", null);

            int S = db.Articles.Where(o => o.CreatedAt >= StartDate).Count();
            int E = db.Articles.Where(o => o.CreatedAt > EndDate).Count();
            List<Article> Success= db.Articles
                .SqlQuery("select * from Articles where CreatedAt >=' "+Start+" '")
                .ToList();

            int Ok = 0;
            int NotYet = 0;
            int Error = 0;
            foreach(Article Ar in Success)
            {
                DateTime Artime = (DateTime)Ar.EditedAt;
                if ((Ar.Status.ToString().Equals("ACTIVE"))&&(Ar.EditedAt!=null)&&(DateTime.Compare(Artime, EndDate)<0))
                {
                    Ok++;
                }
                if ((Ar.Status.ToString().Equals("INITIAL")) && (Ar.EditedAt != null) && (DateTime.Compare(Artime, EndDate) < 0))
                {
                    NotYet++;
                }
                if((Ar.Status.ToString().Equals("DEACTIVE")) && (Ar.EditedAt != null) && (DateTime.Compare(Artime, EndDate) < 0))
                {
                    Error++;
                }
            }
            Debug.WriteLine("total: " +(S-E));
            ReportBoot result = new ReportBoot()
            {
                TotalUrl = S - E,
                Success = Ok,
                Error = Error,
                NotYet = NotYet,          

            };
            return result;
        }
        public ReportBoot DailyReport()
        {
            Debug.WriteLine("===Start DailyReport===");
            String DateNow = DateTime.Now.ToString("yyyy/MM/dd");
            Debug.WriteLine("report at: "+ DateNow);
            int T = db.Articles.SqlQuery("select * from Articles where CreatedAt >= '"+DateNow+"'").Count();
            Debug.WriteLine("sql: " +T);
            int S = db.Articles.SqlQuery("select * from Articles where CreatedAt >= '" + DateNow + "' and status=2").Count();
            int E = db.Articles.SqlQuery("select * from Articles where CreatedAt >= '" + DateNow + "' and status=0").Count();
            int N = db.Articles.SqlQuery("select * from Articles where CreatedAt >= '" + DateNow + "' and status=1").Count();

            ReportBoot result = new ReportBoot()
            {
                TotalUrl = T,
                Success = S,
                Error = E,
                NotYet = N,

            };
            return result;
        }

    }
}