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
using DivishRam_PassionProject.Models;
using System.Diagnostics;

namespace DivishRam_PassionProject.Controllers
{
    public class PublishersTwoDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PublishersTwoData/GetPublishers
        public IQueryable<Publisher> GetPublishers()
        {
            
            return db.Publishers;
        }

        // GET: api/PublishersTwoData/GetPublisher
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult GetPublisher(int id)
        {
            Publisher Publisher = db.Publishers.Find(id);

            if (Publisher == null)
            {
                return NotFound();
            }

            return Ok(Publisher);
        }

        // POST: api/PublishersTwoData/UpdatePublisher/8
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePublisher(int id, Publisher publisher)
        {
            Debug.WriteLine("I have reached the update publisher method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != publisher.PublisherId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET paramater " + id);
                Debug.WriteLine("POST parameter" + publisher.PublisherId);
                return BadRequest();
            }

            db.Entry(publisher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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


        // GET: api/PublishersTwoData/ListArticles
        [HttpGet]
        [ResponseType(typeof(GameArticleDto))]
        public IHttpActionResult ListArticles()
        {
            List<Publisher> Publishers = db.Publishers.ToList();
            List<PublisherDto> PublisherDtos = new List<PublisherDto>();

            Publishers.ForEach(a => PublisherDtos.Add(new PublisherDto()
            {
                PublisherId = a.PublisherId,
                PublisherName = a.PublisherName
            }));

            return Ok(PublisherDtos);
        }


        // PUT: api/PublishersTwoData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPublisher(int id, Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publisher.PublisherId)
            {
                return BadRequest();
            }

            db.Entry(publisher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        // POST: api/PublishersTwoData/AddPublisher
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult AddPublisher(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Publishers.Add(publisher);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = publisher.PublisherId }, publisher);
        }

        // GET: api/PublishersTwoData/DeletePublisher/1
        [ResponseType(typeof(Publisher))]
        [HttpGet]
        public IHttpActionResult DeletePublisher(int id)
        {
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }

            db.Publishers.Remove(publisher);
            db.SaveChanges();

            return Ok(publisher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PublisherExists(int id)
        {
            return db.Publishers.Count(e => e.PublisherId == id) > 0;
        }
    }
}