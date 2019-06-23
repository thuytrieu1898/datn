using DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DoAnTotNghiep.Controllers
{
    public class TestController : ApiController
    {
        private datnEntities db = new datnEntities();
  
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
    }
}
