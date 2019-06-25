using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DoAnTotNghiep.Models;

namespace DoAnTotNghiep.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class tai_khoanAPIController : ApiController
    {
        private datnEntities db = new datnEntities();

        // GET: api/tai_khoanAPI
        public IQueryable<tai_khoan> Gettai_khoan()
        {
            return db.tai_khoan;
        }

        // GET: api/tai_khoanAPI/5
        //[ResponseType(typeof(tai_khoan))]
        //public IHttpActionResult Gettai_khoan(int id)
        //{
        //    tai_khoan tai_khoan = db.tai_khoan.Find(id);
        //    if (tai_khoan == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tai_khoan);
        //}

        [HttpGet]
        [ResponseType(typeof(tai_khoan))]
        public IHttpActionResult Gettai_khoan(string taikhoan, string matkhau)
        {
            string mahoa = hashMD5(matkhau);
            var tk = from t_khoan in db.tai_khoan
                     where t_khoan.tai_khoan1.Equals(taikhoan) && t_khoan.mat_khau.Equals(mahoa)
                     select new
                     {
                         maTK = t_khoan.ID
                     };

            if (tk == null)
            {
                return NotFound();
            }
            return Ok(tk);
        }

        //Ham ma hoa
        public string hashMD5(string matkhau)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(matkhau));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        // PUT: api/tai_khoanAPI/5
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

        // POST: api/tai_khoanAPI
        [ResponseType(typeof(tai_khoan))]
        public IHttpActionResult Posttai_khoan(tai_khoan tai_khoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            tai_khoan.mat_khau = hashMD5(tai_khoan.mat_khau);
            db.tai_khoan.Add(tai_khoan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tai_khoan.ID }, tai_khoan);
        }

        // DELETE: api/tai_khoanAPI/5
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