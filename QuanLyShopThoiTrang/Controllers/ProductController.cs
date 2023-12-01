using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using QuanLyShopThoiTrang.Models;
namespace QuanLyShopThoiTrang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=MSI\CONG03;Initial Catalog=QuanLyShop;Integrated Security=True");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductPatial()
        {
            var listProDuct = db.Products.ToList();
            return View(listProDuct);
        }
        public ActionResult AllSP()
        {
            return View();
        }
        public ActionResult _PartialCate()
        {
            var listCate = db.Categories.ToList();
            return View(listCate);
        }
        public ActionResult SanPhamNew()
        {
            var listNew = db.Products.Take(10).OrderByDescending(sp => sp.Dateadd).ToList();

            return View(listNew);
        }

        public ActionResult Details(int maSP)
        {
            var sp = db.Products.Where(s => s.product_id == maSP).FirstOrDefault();
            if (sp == null)
            {
                return Content("Khong tim thay");
            }
            return View(sp);
        }

    }
}