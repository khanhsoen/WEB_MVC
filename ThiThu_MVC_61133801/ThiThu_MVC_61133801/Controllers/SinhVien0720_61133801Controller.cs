using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThiThu_MVC_61133801.Models;

namespace ThiThu_MVC_61133801.Controllers
{
    public class SinhVien0720_61133801Controller : Controller
    {
        KT0720_MVC_61133801Entities db = new KT0720_MVC_61133801Entities();
        // GET: SinhVien0720_61133801
        public ActionResult Index()
        {
            //var sinhViens = db.SINHVIEN.Include(x => x.LOP);
            List<SINHVIEN> sinhViens = db.SINHVIEN.ToList();
            return View(sinhViens);
        }
        public ActionResult TimKiem()
        {
            var sinhVien = db.SINHVIEN.Include(b => b.LOP);
            return View(sinhVien);
            //List<NHANVIEN> nv = db.NHANVIEN.ToList();
            //return View(nv);
        }
        [HttpGet]
        public ActionResult TimKiem(string searchString)
        {
            var sinhVien = db.SINHVIEN.Include(b => b.LOP);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                sinhVien = sinhVien.Where(b => b.MaSV.ToLower().Contains(searchString));
            }
            return View(sinhVien.ToList());
        }
        //[HttpGet]
        //public ActionResult TimKiem(string searchString, string lop)
        //{
        //    // 1. Lưu tư khóa tìm kiếm
        //    ViewBag.Keyword1 = lop;
        //    ViewBag.Keyword2 = searchString;
        //    // 2.Tạo câu truy vấn kết 2 bảng SINH VIÊN VÀ LỚP
        //    var sinhVien = db.SINHVIEN.Include(b => b.LOP);

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        sinhVien = sinhVien.Where(b => b.MaSV.Contains(searchString));
        //    }
        //    if (!String.IsNullOrEmpty(lop))
        //    {
        //        lop = lop.ToLower();
        //        sinhVien = sinhVien.Where(b => b.MaLop.ToLower().Contains(lop));
        //    }
        //    ViewBag.sinhvien = new SelectList(db.SINHVIEN, "MaLop", "MaSV");
        //    return View(sinhVien.ToList());
        //}


        string LayMaSV()
        {
            var maMax = db.SINHVIEN.ToList().Select(n => n.MaSV).Max();
            int maSV = int.Parse(maMax.Substring(2)) + 1;
            string SV = String.Concat("0", maSV.ToString());
            return "SV" + SV.Substring(maSV.ToString().Length - 1);
        }
        public ActionResult Create()
        {
            ViewBag.MaSV = LayMaSV();
            ViewBag.MaLop = new SelectList(db.LOP, "MaLop", "TenLop");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SINHVIEN sinhVien)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgSV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgSV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgSV.SaveAs(path);

            if (ModelState.IsValid)
            {
                sinhVien.MaSV = LayMaSV();
                sinhVien.AnhSV = postedFileName;
                db.SINHVIEN.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.LOP, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
            //List<SINHVIEN> sinhVien = db.SINHVIEN.ToList();
            //if (ModelState.IsValid)
            //{
            //    db.SINHVIEN.Add(sinhVien);
            //   db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.MaPB = new SelectList(db.LOP, "MaLop", "TenLop", sinhVien.MaLop);
            //return View(sinhVien)
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sinhVien = db.SINHVIEN.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sinhVien = db.SINHVIEN.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.LOP, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SINHVIEN sinhVien)
        {
            var imgNV = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgNV.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LOP, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
            // hiển thị bình thường không có chọn ảnh
            //if (ModelState.IsValid)
            //{
            //    db.Entry(nhanVien).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.MaPB = new SelectList(db.PHONGBAN, "MaPB", "TenPB", nhanVien.MaPB);
            //return View(nhanVien);
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sinhVien = db.SINHVIEN.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SINHVIEN sinhVien = db.SINHVIEN.Find(id);
            db.SINHVIEN.Remove(sinhVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
