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
    public class ct_bai_lamAPIController : ApiController
    {
        private datnEntities db = new datnEntities();

        // GET: api/ct_bai_lamAPI
        public IQueryable<ct_bai_lam> Getct_bai_lam()
        {
            return db.ct_bai_lam;
        }

        // GET: api/ct_bai_lamAPI/5
        [ResponseType(typeof(ct_bai_lam))]
        public IHttpActionResult Getct_bai_lam(int id)
        {
            ct_bai_lam ct_bai_lam = db.ct_bai_lam.Find(id);
            if (ct_bai_lam == null)
            {
                return NotFound();
            }

            return Ok(ct_bai_lam);
        }

        // PUT: api/ct_bai_lamAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putct_bai_lam(int id, ct_bai_lam ct_bai_lam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ct_bai_lam.id_nguoi_lam)
            {
                return BadRequest();
            }

            db.Entry(ct_bai_lam).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ct_bai_lamExists(id))
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

        
        // POST: api/ct_bai_lamAPI
        [ResponseType(typeof(ct_bai_lam))]
        public IHttpActionResult Postct_bai_lam(ct_bai_lam ct_bai_lam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            db.ct_bai_lam.Add(ct_bai_lam);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ct_bai_lamExists(ct_bai_lam.id_nguoi_lam))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ct_bai_lam.id_nguoi_lam }, ct_bai_lam);
        }

        // DELETE: api/ct_bai_lamAPI/5
        [ResponseType(typeof(ct_bai_lam))]
        public IHttpActionResult Deletect_bai_lam(int id)
        {
            ct_bai_lam ct_bai_lam = db.ct_bai_lam.Find(id);
            if (ct_bai_lam == null)
            {
                return NotFound();
            }

            db.ct_bai_lam.Remove(ct_bai_lam);
            db.SaveChanges();

            return Ok(ct_bai_lam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ct_bai_lamExists(int id)
        {
            return db.ct_bai_lam.Count(e => e.id_nguoi_lam == id) > 0;
        }
    }
}