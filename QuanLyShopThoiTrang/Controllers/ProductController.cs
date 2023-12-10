using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using QuanLyShopThoiTrang.Models;
using System.Dynamic;
using System.Drawing;
using PagedList;

namespace QuanLyShopThoiTrang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductPatial(int page = 1, int pageSize = 10)
        {
            var listProDuct = db.Products.ToPagedList(page, pageSize);
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
            var listVariants = from variant in db.ProductVariants                            
                            where variant.product_id == maSP
                            select variant.ImageSP;
            ViewBag.listVariants = listVariants.Distinct();

            var listColor = from variant in db.ProductVariants
                            join color in db.Colors on variant.color_id equals color.color_id
                            where variant.product_id == maSP
                            select color.color_name ;                                                                                                                   
            ViewBag.listColor =listColor.Distinct();

            var listSize = from variant in db.ProductVariants
                            join size in db.Sizes on variant.size_id equals size.size_id
                            where variant.product_id == maSP
                            select size.size_name;
            ViewBag.listSize = listSize.Distinct();

            return View(sp);
        }
        public ActionResult SearchSanPham(string txt_Search)
        {
            var list = db.Products.Where(s => s.product_name.Contains(txt_Search)).ToList();
            if (list == null)
            {
                ViewBag.TB = "Chưa có sản phẩm này ";
            }
            return View(list);
        }
        public ActionResult ViewAo(int id)
        {
            var list = db.Products.Where(s => s.category_id == id ).ToList();
            return View(list);
        }
        public ActionResult getListImage(int id)
        {
            var listVariants = from variant in db.ProductVariants
                               where variant.product_id == id
                               select variant.ImageSP;
            var list = listVariants.Distinct().Take(3);
            return View(list);
        }
        public ActionResult SanPhamTheoLoai()
        {
            var list = db.Products.ToList();
            return View(list);
        }
    }
}