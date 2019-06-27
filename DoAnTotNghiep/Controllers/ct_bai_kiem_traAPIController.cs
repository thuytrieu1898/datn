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
    public class ct_bai_kiem_traAPIController : ApiController
    {       
        private datnEntities db = new datnEntities();

        // GET: api/ct_bai_kiem_traAPI/Getct_bai_kiem_tra
        public IQueryable<ct_bai_kiem_tra> Getct_bai_kiem_tra()
        {
            var ct_bai_kiem_tra = from ctBKT in db.ct_bai_kiem_tra
                                  select new
                                  {
                                      ID_bkt = ctBKT.id_bkt,
                                      ID_cauhoi = ctBKT.id_cau_hoi,
                                      thutu = ctBKT.thu_tu
                                  };

            return db.ct_bai_kiem_tra;
        }

        // GET: api/ct_bai_kiem_traAPI/Getct_bai_kiem_tra/5
        [ResponseType(typeof(ct_bai_kiem_tra))]
        public IHttpActionResult Getct_bai_kiem_tra(int id)
        {
            ct_bai_kiem_tra ct_bai_kiem_tra = db.ct_bai_kiem_tra.Find(id);
            if (ct_bai_kiem_tra == null)
            {
                return NotFound();
            }

            return Ok(ct_bai_kiem_tra);
        }

        // PUT: api/ct_bai_kiem_traAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putct_bai_kiem_tra(int id, ct_bai_kiem_tra ct_bai_kiem_tra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ct_bai_kiem_tra.id_bkt)
            {
                return BadRequest();
            }

            db.Entry(ct_bai_kiem_tra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ct_bai_kiem_traExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ct_bai_kiem_tra);
        }

        // POST: api/ct_bai_kiem_traAPI/Postct_bai_kiem_tra
        [ResponseType(typeof(ct_bai_kiem_tra))]
        public IHttpActionResult Postct_bai_kiem_tra(ct_bai_kiem_tra ct_bai_kiem_tra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ct_bai_kiem_tra.Add(ct_bai_kiem_tra);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ct_bai_kiem_traExists(ct_bai_kiem_tra.id_bkt))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ct_bai_kiem_tra.id_bkt }, ct_bai_kiem_tra);
        }

        // DELETE: api/ct_bai_kiem_traAPI/5
        [ResponseType(typeof(ct_bai_kiem_tra))]
        public IHttpActionResult Deletect_bai_kiem_tra(int id)
        {
            ct_bai_kiem_tra ct_bai_kiem_tra = db.ct_bai_kiem_tra.Find(id);
            if (ct_bai_kiem_tra == null)
            {
                return NotFound();
            }

            db.ct_bai_kiem_tra.Remove(ct_bai_kiem_tra);
            db.SaveChanges();

            return Ok(ct_bai_kiem_tra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ct_bai_kiem_traExists(int id)
        {
            return db.ct_bai_kiem_tra.Count(e => e.id_bkt == id) > 0;
        }
    }
}