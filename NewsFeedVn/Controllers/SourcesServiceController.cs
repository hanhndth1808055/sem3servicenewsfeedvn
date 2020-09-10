using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewsFeedVn.Models;

namespace NewsFeedVn.Controllers
{
    public class SourcesServiceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SourcesService
        public List<Source> GetSources()
        {
            return db.Sources.ToList();
        }

        // GET: api/SourcesService/5
        [ResponseType(typeof(Source))]
        public IHttpActionResult GetSource(int id)
        {
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return NotFound();
            }

            return Ok(source);
        }

        // PUT: api/SourcesService/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSource(int id, Source source)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != source.Id)
            {
                return BadRequest();
            }

            db.Entry(source).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(source);
        }

        // POST: api/SourcesService
        [ResponseType(typeof(Source))]
        public IHttpActionResult PostSource(Source source)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.Sources.Add(source);
                            db.SaveChanges();
            }catch(Exception ex)
            {
                InternalServerError(ex.Message);
            }
            Category Ca = db.Categories.Find(source.CategoryID);
            source.Category = Ca;
            return Ok(source);
        }

        private void InternalServerError(string message)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/SourcesService/5
        [ResponseType(typeof(Source))]
        public IHttpActionResult DeleteSource(int id)
        {
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return NotFound();
            }

            db.Sources.Remove(source);
            db.SaveChanges();

            return Ok(source);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SourceExists(int id)
        {
            return db.Sources.Count(e => e.Id == id) > 0;
        }
    }
}