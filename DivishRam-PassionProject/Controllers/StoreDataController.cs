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
    public class StoreDataController: ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        ///returns all stores in the system
        ///Header: 200 (ok)
        ///Content: all stores
        ///Get: api/StoreData/ListStores
        ///
        [HttpGet]
        [ResponseType(typeof(StoreDto))]
        public IHttpActionResult ListStores()
        {
            List<Store> Stores = db.Stores.ToList();
            List<StoreDto> StoreDtos = new List<StoreDto>();

            Stores.ForEach(s => StoreDtos.Add(new StoreDto()
            {
                StoreId = s.StoreId,
                StoreName = s.StoreName
            }));

            return Ok(StoreDtos);
        }


        ///update keeper with post data input
        ///param name = "id"
        ///param name = "store"
        ///header 204 (Success, not content response)
        ///Header: 404 (not found)
        ///Post: api/StoreData/UpdateStore/1
        ///Form data: store JSon Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateStore(int id, Store Store)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != Store.StoreId)
            {
                return BadRequest();
            }

            db.Entry(Store).State = EntityState.Modified;

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

        ///return store by id
        ///header: 200 (ok)
        ///content: a store with store primary key
        ///Header 404 (not found)
        ///param name = "id"
        ///example
        ///Get: api/StoreData/FindStore/4
        [ResponseType(typeof(StoreDto))]
        [HttpGet]
        public IHttpActionResult FindStore(int id)
        {
            Store Store = db.Stores.Find(id);
            StoreDto StoreDto = new StoreDto()
            {
                StoreId = Store.StoreId,
                StoreName = Store.StoreName
            };
            if(Store == null)
            {
                return NotFound();
            }
            return Ok(StoreDto);

        }

        
        ///Add store to the system
        ///param name ="store" 
        ///Header 201: Created
        ///or Header 400 (bad request)
        ///POST api/StoreData/AddStore
        ///Form data: store json object
        [ResponseType(typeof(Store))]
        [HttpPost]
        public IHttpActionResult AddStore(Store Store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(Store);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = Store.StoreId }, Store);

        }

        ///Delete store by its ID
        ///param name ="id"
        ///header: 200 (ok)
        ///or
        ///Header: 404 (Not found)
        ///Post: api/StoreData/DeleteStore/3
        ///Form data: (empty)
        [ResponseType(typeof(Store))]
        [HttpPost]
        public IHttpActionResult DeleteStore(int id)
        {
            Store Store = db.Stores.Find(id);
            if (Store == null)
            {
                return NotFound();
            }
            db.Stores.Remove(Store);
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

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.StoreId == id) > 0;
        }


    }

}