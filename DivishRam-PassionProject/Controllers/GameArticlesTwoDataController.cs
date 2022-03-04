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
    public class GameArticlesTwoDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GameArticlesTwoData/GetGameArticles
        [HttpGet]
        public IHttpActionResult GetGameArticles()
        {
            List<GameArticle> GameArticles = db.GameArticles.ToList();
            List<GameArticleDto> GameArticleDtos = new List<GameArticleDto>();

            GameArticles.ForEach(a => GameArticleDtos.Add(new GameArticleDto()
            {
                ArticleId = a.ArticleId,
                Title = a.Title,
                ReleaseYear = a.ReleaseYear,
                Rating = a.Rating,
                Author = a.Author,
                Summary = a.Summary,
                //PublisherId = a.PublisherId

            }));
            return Ok(GameArticleDtos);
        }

        // GET: api/GameArticlesTwoData/GetGameArticle
        [ResponseType(typeof(GameArticle))]
        public IHttpActionResult GetGameArticle(int id)
        {
            GameArticle GameArticle = db.GameArticles.Find(id);
            GameArticleDto GameArticleDto = new GameArticleDto()
            {
                ArticleId = GameArticle.ArticleId,
                Author = GameArticle.Author,
                Rating = GameArticle.Rating,
                Summary = GameArticle.Summary,
                Title = GameArticle.Title,
                ReleaseYear = GameArticle.ReleaseYear
            };

            if (GameArticle == null)
            {
                return NotFound();
            }

            return Ok(GameArticleDto);
        }

        // PUT: api/GameArticlesTwoData/PutGameArticle/5
        [ResponseType(typeof(void))]
        [HttpPost]
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

        // POST: api/GameArticlesTwoData/UpdateArticle
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateArticle(int id, GameArticle GameArticle)
        {
            Debug.WriteLine("I have reached the update game article method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != GameArticle.ArticleId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET paramater " + id);
                Debug.WriteLine("POST parameter" + GameArticle.ArticleId);
                return BadRequest();
            }

            db.Entry(GameArticle).State = EntityState.Modified;

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



        // POST: api/GameArticlesTwoData/AddGameArticle
        [ResponseType(typeof(GameArticle))]
        public IHttpActionResult AddGameArticle(GameArticle gameArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameArticles.Add(gameArticle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gameArticle.ArticleId }, gameArticle);
        }


        // GET: api/GameArticlesTwoData/DeleteGameArticle
        [ResponseType(typeof(GameArticle))]
        [HttpGet]
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