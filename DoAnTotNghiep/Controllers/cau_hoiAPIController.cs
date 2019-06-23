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
    public class cau_hoiAPIController : ApiController
    {
        private datnEntities db = new datnEntities();

        // GET: api/cau_hoiAPI/Getcau_hoi
        [ResponseType(typeof(void))]
        public IHttpActionResult Getcau_hoi()
        {
            var cau_hoi = from ch in db.cau_hoi
                          select new
                          {
                              id = ch.ID,
                              Mota = ch.mo_ta,
                              DaA = ch.dap_an_a,
                              DaB = ch.dap_an_b,
                              DaC = ch.dap_an_c,
                              DaD = ch.dap_an_d
                          };
            return Ok(cau_hoi);
        }

        // GET: api/cau_hoiAPI/Getcau_hoi/5
        [ResponseType(typeof(cau_hoi))]
        public IHttpActionResult Getcau_hoi(int id)
        {
            var cau_hoi = from ch in db.cau_hoi
                              where ch.ID.Equals(id)
                              select new
                              {
                                  Mota = ch.mo_ta,
                                  DaA = ch.dap_an_a,
                                  DaB = ch.dap_an_b,
                                  DaC = ch.dap_an_c,
                                  DaD = ch.dap_an_d
                              };
            if (cau_hoi == null)
            {
                return NotFound();
            }

            return Ok(cau_hoi);
        }

        // PUT: api/cau_hoiAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcau_hoi(int id, cau_hoi cau_hoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cau_hoi.ID)
            {
                return BadRequest();
            }

            db.Entry(cau_hoi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cau_hoiExists(id))
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

        // POST: api/cau_hoiAPI
        [ResponseType(typeof(cau_hoi))]
        public IHttpActionResult Postcau_hoi(cau_hoi cau_hoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cau_hoi.Add(cau_hoi);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cau_hoi.ID }, cau_hoi);
        }

        // DELETE: api/cau_hoiAPI/5
        [ResponseType(typeof(cau_hoi))]
        public IHttpActionResult Deletecau_hoi(int id)
        {
            cau_hoi cau_hoi = db.cau_hoi.Find(id);
            if (cau_hoi == null)
            {
                return NotFound();
            }

            db.cau_hoi.Remove(cau_hoi);
            db.SaveChanges();

            return Ok(cau_hoi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cau_hoiExists(int id)
        {
            return db.cau_hoi.Count(e => e.ID == id) > 0;
        }
    }
}