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
    public class DoTestController : ApiController
    {
        private datnEntities db = new datnEntities();

        //Truyền vào ma_bkt và ID người làm lấy ra DS câu hỏi trong BKT đó
        //api/DoTest/TestCauHoi
        [HttpGet]
        [ResponseType(typeof(ct_bai_lam))]
        public IHttpActionResult TestCauHoi(int ID, string ma_bkt)
        {
            var ctbai_lam = from ct in db.ct_bai_lam
                            join bkt in db.bai_kiem_tra on ct.id_bai_kt equals bkt.ID
                            join tk in db.tai_khoan on ct.id_nguoi_lam equals tk.ID
                            join ch in db.cau_hoi on ct.id_cau_hoi equals ch.ID
                            where tk.ID.Equals(ID) && bkt.ma_bkt.Equals(ma_bkt)
                            select new
                            {
                                id = ch.ID,
                                DAChon = ct.dap_an_chon
                            };
            if (ctbai_lam == null)
            {
                return NotFound();
            }

            return Ok(ctbai_lam);
        }
        
    }
}
