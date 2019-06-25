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
    public class bai_kiem_traAPIController : ApiController
    {
        private datnEntities db = new datnEntities();

        // GET: api/bai_kiem_traAPI/Getbai_kiem_tra
        [ResponseType(typeof(bai_kiem_tra))]
        public IHttpActionResult Getbai_kiem_tra()
        {
            var bai_kiem_tra = from bkt in db.bai_kiem_tra                          
                               select new
                               {
                                   id = bkt.ID,
                                   NguoiTao = bkt.nguoi_tao,
                                   ChuDe = bkt.chu_de,
                                   Mabkt = bkt.ma_bkt
                               };
            return Ok(bai_kiem_tra);
        }

        // GET: api/bai_kiem_traAPI/Getbai_kiem_tra/5
        [ResponseType(typeof(bai_kiem_tra))]
        public IHttpActionResult Getbai_kiem_tra(int id)
        {
            var bai_kiem_tra = from bkt in db.bai_kiem_tra
                               join ct in db.ct_bai_kiem_tra on bkt.ID equals ct.id_bkt
                               join ch in db.cau_hoi on ct.id_cau_hoi equals ch.ID
                               where bkt.ID.Equals(id)
                               select new
                               {
                                   MoTa = ch.mo_ta,
                                   DaA = ch.dap_an_a,
                                   DaB = ch.dap_an_b,
                                   Dac = ch.dap_an_c,
                                   DaD = ch.dap_an_d
                               };
            //var baikiemtra = from bkt in db.bai_kiem_tra.Include(i => i.ct_bai_kiem_tra)
            //                 where bkt.ID.Equals(id)
            //                 select bkt;
            //return Ok(baikiemtra);
            if (bai_kiem_tra == null)
            {
                return NotFound();
            }

            return Ok(bai_kiem_tra);
        }

        // PUT: api/bai_kiem_traAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbai_kiem_tra(int id, bai_kiem_tra bai_kiem_tra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bai_kiem_tra.ID)
            {
                return BadRequest();
            }

            db.Entry(bai_kiem_tra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bai_kiem_traExists(id))
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

        // POST: api/bai_kiem_traAPI/Postbai_kiem_tra
        [ResponseType(typeof(bai_kiem_tra))]
        public IHttpActionResult Postbai_kiem_tra(bai_kiem_tra bai_kiem_tra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bai_kiem_tra.Add(bai_kiem_tra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bai_kiem_tra.ID }, bai_kiem_tra);
        }

        // DELETE: api/bai_kiem_traAPI/5
        [ResponseType(typeof(bai_kiem_tra))]
        public IHttpActionResult Deletebai_kiem_tra(int id)
        {
            bai_kiem_tra bai_kiem_tra = db.bai_kiem_tra.Find(id);
            if (bai_kiem_tra == null)
            {
                return NotFound();
            }

            db.bai_kiem_tra.Remove(bai_kiem_tra);
            db.SaveChanges();

            return Ok(bai_kiem_tra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bai_kiem_traExists(int id)
        {
            return db.bai_kiem_tra.Count(e => e.ID == id) > 0;
        }
    }
}