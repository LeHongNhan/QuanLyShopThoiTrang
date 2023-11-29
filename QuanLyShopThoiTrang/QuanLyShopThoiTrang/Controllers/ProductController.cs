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
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext();
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
        
    }
}