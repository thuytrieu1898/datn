using DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DoAnTotNghiep.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        private datnEntities db = new datnEntities();

        [HttpPost]
        public IHttpActionResult Login([FromBody] tai_khoan tai_khoan)
        {
            string mahoa = hashMD5(tai_khoan.mat_khau);
            var tk = from t_khoan in db.tai_khoan
                     where t_khoan.tai_khoan1.Equals(tai_khoan.tai_khoan1) && t_khoan.mat_khau.Equals(mahoa)
                     select new
                     {
                         maTK = t_khoan.ID,
                         loai = t_khoan.loai_tai_khoan
                     };

            if (tk == null)
            {
                return NotFound();
            }
            return Ok(tk.FirstOrDefault());
        }

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
    }
}
