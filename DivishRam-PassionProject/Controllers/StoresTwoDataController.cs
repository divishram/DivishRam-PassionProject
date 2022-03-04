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
    public class StoresTwoDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StoresTwoData/GetStores
        public IQueryable<Store> GetStores()
        {
            return db.Stores;
        }

        // GET: api/StoresTwoData/GetStore
        [ResponseType(typeof(Store))]
        public IHttpActionResult GetStore(int id)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/StoresTwoData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.StoreId)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/StoresTwoData/AddStore
        [ResponseType(typeof(Store))]
        public IHttpActionResult AddStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = store.StoreId }, store);
        }

        // POST: api/StoresTwoData/UpdateStore/8
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateStore(int id, Store store)
        {
            Debug.WriteLine("I have reached the update store method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != store.StoreId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET paramater " + id);
                Debug.WriteLine("POST parameter" + store.StoreId);
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // GET: api/StoresTwoData/DeleteStore/1
        [ResponseType(typeof(Store))]
        [HttpGet]
        public IHttpActionResult DeleteStore(int id)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            db.SaveChanges();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.StoreId == id) > 0;
        }
    }
}