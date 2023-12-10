using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Areas.Admin.Controllers
{
    public class CateProductController : Controller
    {
        // GET: Admin/CateProduct
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");

        public ActionResult CateProduct()
        {
            var listCate = db.Categories.ToList();
            return View(listCate);

        }
        public ActionResult AddCate()
        {
          
            return View();

        }
        [HttpPost]
        public ActionResult AddCate([Bind(Include = "category_name")] Category p)
        {
            if (ModelState.IsValid)
            {
                db.Categories.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            return RedirectToAction("CateProduct");
        }
        public ActionResult UpdateCate(int id)
        {
            Category product = db.Categories.Where(row => row.category_id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult UpdateCate(Category pr)
        {
            if (ModelState.IsValid)
            {
                Category product = db.Categories.Where(row => row.category_id == pr.category_id).FirstOrDefault();
                //Update
               
                product.category_name = pr.category_name;
              
                db.SubmitChanges();
            }
            return RedirectToAction("CateProduct");
        }

        public ActionResult DeleteCate(int id)
        {
            Category product = db.Categories.Where(row => row.category_id == id).FirstOrDefault();
            return View(product);

        }
        [HttpPost]
        public ActionResult DeleteCate(Category p)
        {
            Category product = db.Categories.Where(row => row.category_id == p.category_id).FirstOrDefault();
            if (product == null)
            {
                //nếu xóa không thành công trả về giáo diện đang xóa hoặc giao diện nào z?
                ViewBag.ErrorDelete = "Xóa không thành công !";
                return RedirectToAction("CateProduct");
            }
            else
            {
                try
                {
                    db.Categories.DeleteOnSubmit(product);
                    db.SubmitChanges();
                }
                catch
                {
                    TempData["errorDelete"] = "Xóa không thành công - có liên kết khóa ngoại!";
                    return RedirectToAction("CateProduct");

                }
                return RedirectToAction("CateProduct");
            }
        }
        public ActionResult ThemMauSP()
        {
            List<Product> product = db.Products.ToList();
            ViewBag.product = product;
            List<Size> sizes = db.Sizes.ToList();
            ViewBag.sizes = sizes;
            List<Color> colors = db.Colors.ToList();
            ViewBag.colors = colors;
            return View();
        }
    }
}