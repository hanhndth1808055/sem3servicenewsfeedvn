using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using NewsFeedVn.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static NewsFeedVn.Models.Article;

namespace NewsFeedVn.service
{
    public class Boot2 : IJob
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                GetDataDetail();

            });
            return task;
        }

        public void GetDataDetail()
        {
            Debug.WriteLine("start get Detail news");
            try
            {
                DateTime date_now = DateTime.Now;
                Debug.WriteLine("Get data from" + date_now.AddDays(-1).ToString("yyyy/MM/dd"));

                List<Article> articles = db.Articles
                    .SqlQuery("Select * from Articles where CreatedAt >' " + date_now.AddDays(-2).ToString("yyyy/MM/dd")+" '")
                    .ToList<Article>();

                //List<Article> articles = db.Articles.ToList();
                Debug.WriteLine("Start get url");
                for (int i = 0; i < articles.Count; i++)
                {
                    Debug.WriteLine(articles[i].Status);
                    if (articles[i].Status.ToString().Equals("INITIAL"))
                    {
                        var web = new HtmlAgilityPack.HtmlWeb();
                        var document = web.Load(articles[i].Url);
                        var page = document.DocumentNode;

                        Source source = db.Sources.Find(articles[i].SourceId);
                        //Debug.WriteLine(source.TitleSelector);
                        try
                        {
                            String title = page.QuerySelector(source.TitleSelector).InnerHtml;
                            String description = page.QuerySelector(source.DescriptionSelector).InnerHtml;
                            var content = page.QuerySelector(source.ContentSelector);
                            var imgNodes = content.SelectNodes("//img[@data-src]");

                            String linkImg = "";
                            if (null != imgNodes)
                            {
                                foreach (var itemImg in imgNodes)
                                {
                                    var imgLink = itemImg.Attributes["data-src"].Value;
                                    var newImgNode = $"<img src='{imgLink}'/>";
                                    var newNode = HtmlNode.CreateNode(newImgNode);
                                    itemImg.ParentNode.ReplaceChild(newNode, itemImg);
                                }
                                var imgNodes2 = content.SelectNodes("//img[@src]");
                                var test2 = content.QuerySelector("img");
                                linkImg = test2.Attributes["src"].Value.ToString();
                            }
                            else
                            {
                                var imgNodes2 = content.SelectNodes("//img[@src]");
                                var test2 = content.QuerySelector("img");
                                linkImg = test2.Attributes["src"].Value.ToString();
                                Debug.WriteLine(" link ảnh m cần đây này: " + linkImg);
                                foreach (var itemImg in imgNodes2)
                                {
                                    var imgLink = itemImg.Attributes["src"].Value;
                                    var newImgNode = $"<img src='{imgLink}'/>";
                                    var newNode = HtmlNode.CreateNode(newImgNode);
                                    itemImg.ParentNode.ReplaceChild(newNode, itemImg);
                                }
                            }
                            if (linkImg.StartsWith("/"))
                            {
                                linkImg = source.Domain.TrimEnd('/') + linkImg;
                            }
                            if (linkImg==null)
                            {
                                linkImg = "https://i.pinimg.com/originals/ce/5f/9b/ce5f9be1e1344c5c73c68ae534ca7c66.jpg";
                            }
                            string contenResult = content.InnerHtml;
                            Debug.WriteLine("content: " + contenResult);
                            if (title != null && title != "" &&
                                contenResult != null && contenResult != "")
                            {
                                articles[i].Title = title;
                                articles[i].Description = description;
                                articles[i].Content = contenResult;
                                articles[i].Img = linkImg;
                                articles[i].Status = ArticleStatus.ACTIVE;
                                articles[i].EditedAt = DateTime.Now;
                            }
                            else
                            {
                                articles[i].Status = ArticleStatus.DEACTIVE;
                            }
                        }catch(Exception ex)
                        {
                            Debug.WriteLine("Not get detail data from ArticlesId: " + articles[i].Id);
                            Debug.WriteLine(ex.Message);
                        }
                        
                        db.Entry(articles[i]).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                            Debug.WriteLine("Get data from url: " + document + " done");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("ERROR Update Articles. \n"+ex.Message);
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
            }
            return builder.ToString();
        }
        static string ConvertStringArrayToStringImg(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append('>');
                builder.Append(value);
                
            }
            return builder.ToString();
        }
    }
}