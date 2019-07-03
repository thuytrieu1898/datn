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

        //Lấy ra bài kiểm tra
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

        //Lấy bài kiểm tra theo id
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
            
            if (bai_kiem_tra == null)
            {
                return NotFound();
            }

            return Ok(bai_kiem_tra);
        }

        //Truyền vào ID bài kiểm tra load lên DS SV đã làm bài KT đó và điểm
        //Get: api/bai_kiem_traAPI/DSSV
        [ResponseType(typeof(bai_kiem_tra))]
        public IHttpActionResult DSSV(int ID)
        {
            var lstSV = from bkt in db.bai_kiem_tra
                        join bl in db.bai_lam on bkt.ID equals bl.id_bai_kt
                        join tk in db.tai_khoan on bl.id_nguoi_lam equals tk.ID
                        where bkt.ID.Equals(ID)
                        select new
                        {
                            sinhvien = tk.ten,
                            diem = bl.tong_diem
                        };
                      
            if (lstSV == null)
            {
                return NotFound();
            }

            return Ok(lstSV.ToList());
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
            return Ok(bai_kiem_tra);
            //return StatusCode(HttpStatusCode.NoContent);
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

        [HttpGet]
        public IHttpActionResult CheckKiemTraExist(string ma)
        {
            var bai_kt = from bkt in db.bai_kiem_tra
                         where bkt.ma_bkt.Equals(ma)
                         where bkt.trang_thai == 1
                         select new
                         {
                             ID = bkt.ID
                         };
            return Ok(bai_kt.FirstOrDefault());
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