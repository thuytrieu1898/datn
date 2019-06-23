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
using DoAnTotNghiep.Models;

namespace DoAnTotNghiep.Controllers
{
    public class tai_khoanController : ApiController
    {
        private datnEntities db = new datnEntities();

        // GET: api/tai_khoan
        public IQueryable<tai_khoan> Gettai_khoan()
        {
            return db.tai_khoan;
        }

        // GET: api/tai_khoan/5
        [ResponseType(typeof(tai_khoan))]
        public IHttpActionResult Gettai_khoan(int id)
        {
            tai_khoan tai_khoan = db.tai_khoan.Find(id);
            if (tai_khoan == null)
            {
                return NotFound();
            }

            return Ok(tai_khoan);
        }

        // PUT: api/tai_khoan/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttai_khoan(int id, tai_khoan tai_khoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tai_khoan.ID)
            {
                return BadRequest();
            }

            db.Entry(tai_khoan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tai_khoanExists(id))
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

        // POST: api/tai_khoan
        [ResponseType(typeof(tai_khoan))]
        public IHttpActionResult Posttai_khoan(tai_khoan tai_khoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tai_khoan.Add(tai_khoan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tai_khoan.ID }, tai_khoan);
        }

        // DELETE: api/tai_khoan/5
        [ResponseType(typeof(tai_khoan))]
        public IHttpActionResult Deletetai_khoan(int id)
        {
            tai_khoan tai_khoan = db.tai_khoan.Find(id);
            if (tai_khoan == null)
            {
                return NotFound();
            }

            db.tai_khoan.Remove(tai_khoan);
            db.SaveChanges();

            return Ok(tai_khoan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tai_khoanExists(int id)
        {
            return db.tai_khoan.Count(e => e.ID == id) > 0;
        }
    }
}