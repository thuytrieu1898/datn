using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DoAnTotNghiep.Models;

namespace DoAnTotNghiep.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class chu_deAPIController : ApiController
    {
        private datnEntities db = new datnEntities();

        //lấy danh sách chủ đề
        // GET: api/chu_deAPI/Getchu_de
        [ResponseType(typeof(chu_de))]
        public IHttpActionResult Getchu_de()
        {
            var chu_de = from cd in db.chu_de
                               select new
                               {
                                   id = cd.ID,
                                   TenCD = cd.ten_chu_de
                               };
            return Ok(chu_de);
        }

        //lấy danh sách chủ đề theo ID
        // GET: api/chu_deAPI/Getchu_de/5
        [ResponseType(typeof(chu_de))]
        public IHttpActionResult Getchu_de(int id)
        {
            var chu_de = from cd in db.chu_de
                         where cd.ID.Equals(id)
                         select new
                         {                             
                             TenCD = cd.ten_chu_de
                         };

            if (chu_de == null)
            {
                return NotFound();
            }

            return Ok(chu_de.FirstOrDefault());
        }

        // PUT: api/chu_deAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putchu_de(int id, chu_de chu_de)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chu_de.ID)
            {
                return BadRequest();
            }

            db.Entry(chu_de).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!chu_deExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(chu_de);
            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/chu_deAPI/Postchu_de
        [ResponseType(typeof(chu_de))]
        public IHttpActionResult Postchu_de(chu_de chu_de)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.chu_de.Add(chu_de);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chu_de.ID }, chu_de);
        }

        // DELETE: api/chu_deAPI/5
        [ResponseType(typeof(chu_de))]
        public IHttpActionResult Deletechu_de(int id)
        {
            chu_de chu_de = db.chu_de.Find(id);
            if (chu_de == null)
            {
                return NotFound();
            }

            db.chu_de.Remove(chu_de);
            db.SaveChanges();

            return Ok(chu_de);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool chu_deExists(int id)
        {
            return db.chu_de.Count(e => e.ID == id) > 0;
        }
    }
}