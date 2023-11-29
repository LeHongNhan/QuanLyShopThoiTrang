using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Controllers
{
    public class DNController : Controller
    {
        // GET: DN
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult QuenMatKhau()
        {
            return View();
        }

    }
}