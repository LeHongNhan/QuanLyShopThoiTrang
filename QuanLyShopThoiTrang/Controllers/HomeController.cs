using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyShopThoiTrang.Models;
namespace QuanLyShopThoiTrang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PatialNEWARRIVALS()
        {
            var latestProducts = db.Products
        //.OrderByDescending(p => p.product_id)
        .Take(16)
        .ToList();
            return View(latestProducts);
        }
        public ActionResult _PatialOutstanding()
        {
            var latestProducts = db.Products
            //.OrderByDescending(p => p.price)
            .Take(9)
            .ToList();
            return View(latestProducts);
        }
    }
}