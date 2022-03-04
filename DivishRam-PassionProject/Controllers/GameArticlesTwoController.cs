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

namespace DivishRam_PassionProject.Controllers
{
    public class GameArticlesTwoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GameArticlesTwo
        public IQueryable<GameArticle> GetGameArticles()
        {
            return db.GameArticles;
        }

        // GET: api/GameArticlesTwo/5
        [ResponseType(typeof(GameArticle))]
        public IHttpActionResult GetGameArticle(int id)
        {
            GameArticle gameArticle = db.GameArticles.Find(id);
            if (gameArticle == null)
            {
                return NotFound();
            }

            return Ok(gameArticle);
        }

        // PUT: api/GameArticlesTwo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGameArticle(int id, GameArticle gameArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameArticle.ArticleId)
            {
                return BadRequest();
            }

            db.Entry(gameArticle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameArticleExists(id))
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

        // POST: api/GameArticlesTwo
        [ResponseType(typeof(GameArticle))]
        public IHttpActionResult PostGameArticle(GameArticle gameArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameArticles.Add(gameArticle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gameArticle.ArticleId }, gameArticle);
        }

        // DELETE: api/GameArticlesTwo/5
        [ResponseType(typeof(GameArticle))]
        public IHttpActionResult DeleteGameArticle(int id)
        {
            GameArticle gameArticle = db.GameArticles.Find(id);
            if (gameArticle == null)
            {
                return NotFound();
            }

            db.GameArticles.Remove(gameArticle);
            db.SaveChanges();

            return Ok(gameArticle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameArticleExists(int id)
        {
            return db.GameArticles.Count(e => e.ArticleId == id) > 0;
        }
    }
}