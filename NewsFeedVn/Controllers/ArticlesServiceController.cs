using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewsFeedVn.Models;
using NewsFeedVn.service;

namespace NewsFeedVn.Controllers
{
    public class ArticlesServiceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ArticlesService
        public List<Article> GetArticles()
        {
            return db.Articles.ToList();
        }

        // GET: api/ArticlesService/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        // PUT: api/ArticlesService/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ArticlesService
        [ResponseType(typeof(Article))]
        public IHttpActionResult PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Articles.Add(article);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = article.Id }, article);
        }

        // DELETE: api/ArticlesService/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.Id == id) > 0;
        }
        
    }
}