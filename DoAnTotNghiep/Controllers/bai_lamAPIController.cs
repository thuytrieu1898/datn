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
    public class bai_lamAPIController : ApiController
    {
        private datnEntities db = new datnEntities();

        // GET: api/bai_lamAPI
        public IQueryable<bai_lam> Getbai_lam()
        {
            return db.bai_lam;
        }

        // GET: api/bai_lamAPI/5
        [ResponseType(typeof(bai_lam))]
        public IHttpActionResult Getbai_lam(int id)
        {
            bai_lam bai_lam = db.bai_lam.Find(id);
            if (bai_lam == null)
            {
                return NotFound();
            }

            return Ok(bai_lam);
        }

        // PUT: api/bai_lamAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbai_lam(int id, bai_lam bai_lam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bai_lam.id_nguoi_lam)
            {
                return BadRequest();
            }

            db.Entry(bai_lam).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bai_lamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(bai_lam);
            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/bai_lamAPI
        [ResponseType(typeof(bai_lam))]
        public IHttpActionResult Postbai_lam(bai_lam bai_lam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bai_lam.Add(bai_lam);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (bai_lamExists(bai_lam.id_nguoi_lam))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bai_lam.id_nguoi_lam }, bai_lam);
        }

        // DELETE: api/bai_lamAPI/5
        [ResponseType(typeof(bai_lam))]
        public IHttpActionResult Deletebai_lam(int id)
        {
            bai_lam bai_lam = db.bai_lam.Find(id);
            if (bai_lam == null)
            {
                return NotFound();
            }

            db.bai_lam.Remove(bai_lam);
            db.SaveChanges();

            return Ok(bai_lam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bai_lamExists(int id)
        {
            return db.bai_lam.Count(e => e.id_nguoi_lam == id) > 0;
        }
    }
}