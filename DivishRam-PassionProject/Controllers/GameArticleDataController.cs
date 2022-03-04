
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
    public class GameArticleDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        ///summary
        ///Returns all articles in system
        ///Header: 200 (ok)
        ///Content: all articles and associated publishers
        ///example:
        ///GET: api/GameArticleData/ListArticles
        ///
        [HttpGet]
        [ResponseType(typeof(GameArticleDto))]
        public IHttpActionResult ListArticles()
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
                PublisherId = a.PublisherId
              
            }));
            return Ok(GameArticleDtos);
        }

        ///gets info about articles with particular id
        ///header: 200 (ok)
        ///param name: id Publisher id
        [HttpGet]
        [ResponseType(typeof(GameArticleDto))]
        public IHttpActionResult ListGameArticlesForPublisher(int id)
        {
            List<GameArticle> GameArticles = db.GameArticles.Where(a => a.PublisherId == id).ToList();
            List<GameArticleDto> GameArticleDtos = new List<GameArticleDto>();

            GameArticles.ForEach(a => GameArticleDtos.Add(new GameArticleDto()
            {
                ArticleId = a.ArticleId,
                Title = a.Title,
                ReleaseYear = a.ReleaseYear,
                Rating = a.Rating,
                Author = a.Author,
                Summary = a.Summary,
                PublisherId = a.PublisherId
            }));
            return Ok(GameArticleDtos);       
        }

        ///gathers info about articles related to publisher
        ///header: 200 (ok)
        ///param name = "id"
        ///GET api/GameData/ListGameArticlesForPublishers
        
        /*
        [HttpGet]
        [ResponseType(typeof(GameArticleDto))]
        public IHttpActionResult ListGameArticlesForStores(int id)

            //CHANGE SO IT IS STORE INSTEAD!!!
        {
            List<GameArticle> GameArticles = db.GameArticles.Where(a => a.Stores.Any(k => k.StoreId == id)).ToList();
            List<GameArticleDto> GameArticleDtos = new List<GameArticleDto>();

            GameArticles.ForEach(a => GameArticleDtos.Add(new GameArticleDto()
            {
                ArticleId = a.ArticleId,
                Title = a.Title,
                ReleaseYear = a.ReleaseYear,
                Rating = a.Rating,
                Author = a.Author,
                Summary = a.Summary,
                PublisherId = a.PublisherId                

            }));
            return Ok(GameArticleDtos);
        }
        */

        //associates particular article with store
        //param @articleid
        //param @publisherid
        //header: 200 (ok)
        [HttpPost]
        [Route("api/GameArticleData/AssociateArticleWithPublisher/{articleid}/{publisherid}")]
        public IHttpActionResult AssociateArticleWithPublisher(int articleid, int storeid)
        {
            GameArticle SelectedArticle = db.GameArticles.Include(a => a.Stores).Where(a => a.ArticleId == articleid).FirstOrDefault();
            Store SelectedStore = db.Stores.Find(storeid);

            if (SelectedArticle==null || SelectedStore == null)
            {
                return NotFound();
            }

            Debug.WriteLine("input article id is: " + articleid);
            Debug.WriteLine("selected article title is: " + SelectedArticle.Title);
            Debug.WriteLine("input store id is: " + storeid);
            Debug.WriteLine("selected store name is: " + SelectedStore.StoreName );


            SelectedArticle.Stores.Add(SelectedStore);
            db.SaveChanges();
            return Ok();
        }

        //remove associate btw article and store
        //param @articleid
        //param @storeid
        //Header: 200 (ok)
        //or
        //Header: 404 (Not found)
        [HttpPost]
        [Route("api/GameArticleData/UnAssociateGameArticleWithStore/{articleid}/{storeid}")]
        public IHttpActionResult UnAssociateGameArticleWithStore(int articleid, int storeid)
        {
            GameArticle SelectedArticle = db.GameArticles.Include(a => a.Stores).Where(a => a.ArticleId == articleid).FirstOrDefault();
            Store SelectedStore = db.Stores.Find(storeid);

            if (SelectedArticle == null || SelectedStore == null)
            {
                return NotFound();
            }

            Debug.WriteLine("input article id is: " + articleid);
            Debug.WriteLine("selected article title is: " + SelectedArticle.Title);
            Debug.WriteLine("input store id is: " + storeid);
            Debug.WriteLine("selected store name is: " + SelectedStore.StoreName);

            SelectedArticle.Stores.Remove(SelectedStore);
            db.SaveChanges();
            return Ok();
        }

        ///Returns all articles in the system
        ///Header: 200 (ok)
        ///Content: An aritlce in the system matching up the article id primary key
        ///or
        ///Header 404 (Not found)
        ///param name = "id"
        ///Example
        ///GET: api/GameArticleData/FindArticle/4
        
        /*
        [ResponseType(typeof(GameArticleDto))]
        [HttpGet]
        public IHttpActionResult FindArticle(int id)
        {
            GameArticle GameArticle = db.GameArticles.Find(id);
            GameArticleDto GameArticleDto = new GameArticleDto()

            {
                ArticleId = GameArticle.ArticleId,
                Title = GameArticle.Title,
                ReleaseYear = GameArticle.ReleaseYear,
                Rating = GameArticle.Rating,
                Author = GameArticle.Author,
                Summary = GameArticle.Summary,
                PublisherId = GameArticle.Publisher.PublisherId,
                PublisherName = GameArticle.Publisher.PublisherName
            };

            if (GameArticle == null)
            {
                return NotFound();
            }

            return Ok(GameArticleDto);
        }
        */

        ///Updates article with post data input
        ///param name = "id"
        ///param name = "title" Json form data
        ///Header 204 (Success, no content response)
        ///or
        ///Header: 404 (Bad request)
        ///Post: api/GameArticleData/UpdateArticle/4
        ///Form data: article json object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateArticle(int id, GameArticle gameArticle)
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

            db.SaveChanges();
           // try
            //{
           //     db.SaveChanges();
           // }

            /*    This did not work for me!        
            catch (DbUpdateConcurrencyException)
            {
                if (GameArticle)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return StatusCode(HttpStatusCode.NoContent);
        }

        ///add article to the system
        ///param name ="gamearticle" Json form data of article
        ///header: 201 (Created)
        ///Content article id, article data
        ///or 
        ///Header 400 (Bad request)
        ///Post: api/GameArticleData/AddArticle
        ///For data: Article JSON object
        [ResponseType(typeof(GameArticle))]
        [HttpPost]
        public IHttpActionResult AddArticle(GameArticle gameArticle)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameArticles.Add(gameArticle);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = gameArticle.ArticleId }, gameArticle);
        }

        ///delete article by id from the system
        ///param name = "id"
        ///header: 200(ok)
        ///header: 404 (not found)
        ///example
        ///POST: api/GameArticleData/DeleteArticle/2
        [ResponseType(typeof(GameArticle))]
        [HttpPost]
        public IHttpActionResult DeleteArticle(int id)
        {
            GameArticle gameArticle = db.GameArticles.Find(id);
            if (gameArticle == null)
            {
                return NotFound();
            }

            db.GameArticles.Remove(gameArticle);
            db.SaveChanges();

            return Ok();

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
