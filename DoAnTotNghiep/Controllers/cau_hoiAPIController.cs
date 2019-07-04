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
    public class cau_hoiAPIController : ApiController
    {
        private datnEntities db = new datnEntities();

        //Lấy danh sách tất cả câu hỏi
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
                              DaD = ch.dap_an_d,
                              dung = ch.cau_tra_loi,
                              chu_de = new
                              { 
                                ID = ch.chu_de1.ID,
                                ten_chu_de = ch.chu_de1.ten_chu_de
                              }
                          };
            return Ok(cau_hoi);
        }

        //Lấy danh sách câu hỏi theo ID
        // GET: api/cau_hoiAPI/Getcau_hoi/5
        [ResponseType(typeof(cau_hoi))]
        public IHttpActionResult Getcau_hoi(int id)
        {
            var cau_hoi = from ch in db.cau_hoi
                          where ch.ID.Equals(id)
                          select new
                          {
                              ID = ch.ID,
                              Mota = ch.mo_ta,
                              DaA = ch.dap_an_a,
                              DaB = ch.dap_an_b,
                              DaC = ch.dap_an_c,
                              DaD = ch.dap_an_d,
                              dung = ch.cau_tra_loi,
                              chu_de = new
                              {
                                  ID = ch.chu_de1.ID,
                                  ten_chu_de = ch.chu_de1.ten_chu_de
                              }
                          };
            if (cau_hoi.FirstOrDefault() == null)
            {
                return NotFound();
            }

            return Ok(cau_hoi.FirstOrDefault());
        }

        //Lấy danh sách câu hỏi theo chủ đề
        //GET: api/cau_hoiAPI/GetCH_chu_de
        [ResponseType(typeof(cau_hoi))]
        public IHttpActionResult GetCH_chu_de(int ID)
        {
            var lstCauHoi = from cd in db.chu_de
                            join ch in db.cau_hoi on cd.ID equals ch.chu_de
                            where cd.ID.Equals(ID)
                            select new
                            {
                                ID = ch.ID,
                                Mota = ch.mo_ta,
                                DaA = ch.dap_an_a,
                                DaB = ch.dap_an_b,
                                DaC = ch.dap_an_c,
                                DaD = ch.dap_an_d,
                                dung = ch.cau_tra_loi,
                                chu_de = new
                                {
                                    ID = ch.chu_de1.ID,
                                    ten_chu_de = ch.chu_de1.ten_chu_de
                                }
                            };

            if (lstCauHoi == null)
            {
                return NotFound();
            }

            return Ok(lstCauHoi.ToList());
        }
    

        // PUT: api/cau_hoiAPI/Putcau_hoi/5
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

            return Ok(cau_hoi);
            //return StatusCode(HttpStatusCode.NoContent);
        }

        //tạo POST: api/cau_hoiAPI
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