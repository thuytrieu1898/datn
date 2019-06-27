using DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace DoAnTotNghiep.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AnswerController : ApiController
    {
        private datnEntities db = new datnEntities();

        //post api/bai_lamAPI/Postbai_lam1
        [ResponseType(typeof(bai_lam))]
        public IHttpActionResult Postbai_lam(int id_nguoi_lam, int id_bai_kt, int id_cau_hoi, string dap_an)
        {
            double tong_diem = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //kiểm tra người làm đó đã làm bài trước đó hay chưa
            var bailam = (from bl in db.bai_lam
                          where bl.id_nguoi_lam.Equals(id_nguoi_lam) && bl.id_bai_kt.Equals(id_bai_kt)
                          select bl).FirstOrDefault();

            //kiểm tra đáp án chọn có trùng với đáp an đúng hay không
            var kiemtraTL = db.cau_hoi.FirstOrDefault(x => x.ID.Equals(id_cau_hoi));
            //Định dạng về chữ hoa
            kiemtraTL.cau_tra_loi = kiemtraTL.cau_tra_loi.ToUpper();
            dap_an = dap_an.ToUpper();

            if (kiemtraTL.cau_tra_loi == dap_an)
            {
                tong_diem = 10;
            }
            else
                tong_diem = 0;

            //nếu là lần đầu tiên làm
            if (bailam == null)
            {
                var bl = new bai_lam();
                bl.id_nguoi_lam = id_nguoi_lam;
                bl.id_bai_kt = id_bai_kt;
                bl.tong_diem = tong_diem;
                db.bai_lam.Add(bl);
                int res = db.SaveChanges();
                if (res == 1)
                {
                    var ct = new ct_bai_lam();
                    ct.id_nguoi_lam = id_nguoi_lam;
                    ct.id_bai_kt = id_bai_kt;
                    ct.id_cau_hoi = id_cau_hoi;
                    ct.dap_an_chon = dap_an;
                    db.ct_bai_lam.Add(ct);
                    db.SaveChanges();
                    tong_diem = Convert.ToDouble(bl.tong_diem);
                }
                return Ok(tong_diem);
            }
            else
            {
                //đúng thì cộng dồn lên
                bailam.tong_diem += tong_diem;
                db.Entry(bailam).State = EntityState.Modified;
                int res = db.SaveChanges();
                if (res == 1)
                {
                    var ct = new ct_bai_lam();
                    ct.id_nguoi_lam = id_nguoi_lam;
                    ct.id_bai_kt = id_bai_kt;
                    ct.id_cau_hoi = id_cau_hoi;
                    ct.dap_an_chon = dap_an;
                    db.ct_bai_lam.Add(ct);
                    db.SaveChanges();
                    tong_diem = Convert.ToDouble(bailam.tong_diem);
                    return Ok(tong_diem);
                }
            }
            return BadRequest("fail");
        }
    }
}
