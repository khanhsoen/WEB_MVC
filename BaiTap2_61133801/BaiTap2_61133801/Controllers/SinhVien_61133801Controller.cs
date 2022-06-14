using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap2_61133801.Controllers
{
    public class SinhVien_61133801Controller : Controller
    {
        // GET: SinhVien_61133801
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register2(FormCollection field)
        {
            ViewBag.Id = field["Id"];
            ViewBag.Name = field["Name"];
            ViewBag.Marks = field["Marks"];
            return View(ViewBag);
        }
    }
}