using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaiTap6_61133801.Models;

namespace BaiTap6_61133801.Controllers
{
    public class NhanVienController : Controller
    {
        QLNV_MVC_61133801Entities db = new QLNV_MVC_61133801Entities();
        // GET: NhanVien
        public ActionResult TimKiem()
        {
            var nhanViens = db.NHANVIEN.Include(n => n.PHONGBAN);
            return View(nhanViens.ToList());
        }
        [HttpPost]
        public ActionResult TimKiem(string maNV)
        {

            //var nhanViens = db.NhanViens.SqlQuery("exec NhanVien_DS '"+maNV+"' ");
            /// var nhanViens = db.NhanViens.SqlQuery("SELECT * FROM NhanVien WHERE MaNV='" + maNV + "'");
            var nhanViens = db.NHANVIEN.Where(abc => abc.MaNV == maNV);
            return View(nhanViens.ToList());
        }
        [HttpGet]

        public ActionResult TimKiemNC(string maNV = "", string hoTen = "", string gioiTinh = "", string luongMin = "", string luongMax = "", string diaChi = "", string maPB = "")
        {
            string min = luongMin, max = luongMax;
            if (gioiTinh != "1" && gioiTinh != "0")
                gioiTinh = null;
            ViewBag.maNV = maNV;
            ViewBag.hoTen = hoTen;
            ViewBag.gioiTinh = gioiTinh;
            if (luongMin == "")
            {
                ViewBag.luongMin = "";
                min = "0";
            }
            else
            {
                ViewBag.luongMin = luongMin;
                min = luongMin;
            }
            if (max == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.luongMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.luongMax = luongMax;
                max = luongMax;
            }
            ViewBag.diaChi = diaChi;
            ViewBag.MaPB = new SelectList(db.PHONGBAN, "MaPB", "TenPB");
            var nhanViens = db.NHANVIEN.SqlQuery("NhanVien_TimKiem'" + maNV + "','" + hoTen + "','" + gioiTinh + "','" + min + "','" + max + "',N'" + diaChi + "','" + maPB + "'");
            if (nhanViens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(nhanViens.ToList());
        }
        public ActionResult Index()
       {
            var nhanViens = db.NHANVIEN.Include(b => b.PHONGBAN);
            return View(nhanViens);
            //List<NHANVIEN> nv = db.NHANVIEN.ToList();
            //return View(nv);
        }

        // tìm kiếm theo mã số nhân viên
        [HttpGet]
        public ActionResult Index(string searchString)
        {
            var nhanViens = db.NHANVIEN.Include(b => b.PHONGBAN);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                nhanViens = nhanViens.Where(b => b.MaNV.ToLower().Contains(searchString));
            }
            return View(nhanViens.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nhanVien = db.NHANVIEN.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }
        string LayMaNV()
        {
            var maMax = db.NHANVIEN.ToList().Select(n => n.MaNV).Max();
            int maNV = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("000", maNV.ToString());
            return "NV" + NV.Substring(maNV.ToString().Length - 1);
        }
        public ActionResult Create()
        {
            ViewBag.MaNV = LayMaNV();
            ViewBag.MaPB = new SelectList(db.PHONGBAN, "MaPB", "TenPB");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,Luong,AnhNV,DiaChi,MaPB")] NHANVIEN nhanVien)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgNV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgNV.SaveAs(path);

            if (ModelState.IsValid)
            {
                nhanVien.MaNV = LayMaNV();
                nhanVien.AnhNV = postedFileName;
                db.NHANVIEN.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPB = new SelectList(db.PHONGBAN, "MaPB", "TenPB", nhanVien.MaPB);
            return View(nhanVien);
            //if (ModelState.IsValid)
            //{
            //    db.NHANVIEN.Add(nhanVien);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.MaPB = new SelectList(db.PHONGBAN, "MaPB", "TenPB", nhanVien.MaPB);
            //return View(nhanVien)
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nhanVien = db.NHANVIEN.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPB = new SelectList(db.PHONGBAN, "MaPB", "TenPB", nhanVien.MaPB);
            return View(nhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,Luong,AnhNV,DiaChi,MaPB")] NHANVIEN nhanVien)
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
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPB = new SelectList(db.PHONGBAN, "MaPB", "TenPB", nhanVien.MaPB);
            return View(nhanVien);
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
            NHANVIEN nhanVien = db.NHANVIEN.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHANVIEN nhanVien = db.NHANVIEN.Find(id);
            db.NHANVIEN.Remove(nhanVien);
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