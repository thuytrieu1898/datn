using DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace DoAnTotNghiep.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        private datnEntities db = new datnEntities();
        
        //Truyền vào ma_bkt lấy ra danh sách câu hỏi của BKT đó
        [HttpGet]
        [ResponseType(typeof(bai_kiem_tra))]
        public IHttpActionResult Join(string ma_bkt)
        {
            var bai_kiem_tra = from bkt in db.bai_kiem_tra
                               join ct in db.ct_bai_kiem_tra on bkt.ID equals ct.id_bkt
                               join ch in db.cau_hoi on ct.id_cau_hoi equals ch.ID
                               where bkt.ma_bkt.Equals(ma_bkt)
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

        ////Truyền vào ma_bkt và ID người làm lấy ra DS câu hỏi trong BKT đó
        //[HttpGet]
        //[ResponseType(typeof(bai_kiem_tra))]
        //public IHttpActionResult TestCauHoi(int ID, string ma_bkt)
        //{
        //    var bai_kiem_tra = from bkt in db.bai_kiem_tra
        //                       join ct in db.ct_bai_kiem_tra on bkt.ID equals ct.id_bkt
        //                       join ch in db.cau_hoi on ct.id_cau_hoi equals ch.ID
        //                       where bkt.ID.Equals(ID)
        //                       select new
        //                       {
        //                           id = ch.ID,
        //                           DaA = ch.dap_an_a,
        //                           DaB = ch.dap_an_b,
        //                           Dac = ch.dap_an_c,
        //                           DaD = ch.dap_an_d
        //                       };
        //    if (bai_kiem_tra == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(bai_kiem_tra);
        //}

        //Hàm Random tạo ra ma_bkt
        // api/Test/CreateRandom/1
        [HttpGet]
        [ResponseType(typeof(bai_kiem_tra))]
        public IHttpActionResult CreateRandom(int id)
        {
            var select = db.bai_kiem_tra.FirstOrDefault(x => x.ID.Equals(id));
            if (select != null)
            {
                select.ma_bkt = random(6);
                db.Entry(select).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok(select.ma_bkt);

        }

        private string random(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            char[] lst = new char[length];
            for (int i = 0; i < length; i++)
            {
                lst[i] = chars[random.Next(chars.Length)];
            }

            return new string(lst);
        }
    }
}
