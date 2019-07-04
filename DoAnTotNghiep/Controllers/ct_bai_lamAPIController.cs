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

        [HttpGet]
        public IHttpActionResult DapAnDaChon(int id_nguoi_lam, int id_bai_kt, int id_cau_hoi)
        {
            var dapAn = from ctbl in db.ct_bai_lam
                        where ctbl.id_nguoi_lam.Equals(id_nguoi_lam)
                        where ctbl.id_bai_kt.Equals(id_bai_kt)
                        where ctbl.id_cau_hoi.Equals(id_cau_hoi)
                        select new
                        {
                            answer = ctbl.dap_an_chon
                        };
            return Ok(dapAn.FirstOrDefault());
        }

        [HttpGet]
        public IHttpActionResult CauHoiDaChon(int id_nguoi_lam, int id_bai_kt)
        {
            var dapAn = from ctbl in db.ct_bai_lam
                         where ctbl.id_nguoi_lam.Equals(id_nguoi_lam)
                         where ctbl.id_bai_kt.Equals(id_bai_kt)
                         select new
                         {
                             ctbl.id_cau_hoi
                         };
            return Ok(dapAn.AsEnumerable().Select(i => i.id_cau_hoi).ToArray());
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
            ChamDiem(ct_bai_lam.id_cau_hoi, ct_bai_lam.id_bai_kt, ct_bai_lam.id_nguoi_lam, ct_bai_lam.dap_an_chon);

            return Ok(ct_bai_lam);
            //return StatusCode(HttpStatusCode.NoContent);
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
            ChamDiem(ct_bai_lam.id_cau_hoi, ct_bai_lam.id_bai_kt, ct_bai_lam.id_nguoi_lam, ct_bai_lam.dap_an_chon);
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

        private void ChamDiem(int idCauHoi, int idBaiKT, int idNguoiLam, string dapan)
        {
            cau_hoi cauhoi = db.cau_hoi.FirstOrDefault(x => x.ID.Equals(idCauHoi));
            bai_kiem_tra bkt = db.bai_kiem_tra.FirstOrDefault(x => x.ID.Equals(idBaiKT));
            int so_cau_hoi = bkt.so_cau_hoi.Value;
            double thang_diem = 100.0 / so_cau_hoi;
            if (cauhoi.cau_tra_loi == dapan)
            {
                var bailam = (from bl in db.bai_lam
                              where bl.id_nguoi_lam.Equals(idNguoiLam) && bl.id_bai_kt.Equals(idBaiKT)
                              select bl).FirstOrDefault();
                bailam.tong_diem += thang_diem;
                bailam.tong_diem = Math.Round(bailam.tong_diem.Value, 2);
                if (bailam.tong_diem > 100)
                {
                    bailam.tong_diem = 100;
                }
                db.Entry(bailam).State = EntityState.Modified;
                db.SaveChanges();
            }
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