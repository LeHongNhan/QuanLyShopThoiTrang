using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Areas.Admin.Controllers
{
    public class SizeController : Controller
    {
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");

        public ActionResult SizeProduct()
        {
            var list = db.Sizes.ToList();
            return View(list);

        }
        public ActionResult AddSize()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AddSize([Bind(Include = "Size_name")] Size p)
        {
            if (ModelState.IsValid)
            {
                db.Sizes.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            return RedirectToAction("SizeProduct");
        }
        public ActionResult UpdateSize(int id)
        {
            Size product = db.Sizes.Where(row => row.size_id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult UpdateSize(Size pr)
        {
            if (ModelState.IsValid)
            {
                Size product = db.Sizes.Where(row => row.size_id == pr.size_id).FirstOrDefault();
                //Update

                product.size_name = pr.size_name;

                db.SubmitChanges();
            }
            return RedirectToAction("SizeProduct");
        }

        public ActionResult DeleteSize(int id)
        {
            Size product = db.Sizes.Where(row => row.size_id == id).FirstOrDefault();
            return View(product);

        }
        [HttpPost]
        public ActionResult DeleteSize(Size p)
        {
            Size product = db.Sizes.Where(row => row.size_id == p.size_id).FirstOrDefault();
            if (product == null)
            {
                //nếu xóa không thành công trả về giáo diện đang xóa hoặc giao diện nào z?
                ViewBag.ErrorDelete = "Xóa không thành công !";
                return RedirectToAction("SizeProduct");
            }
            else
            {
                try
                {
                    db.Sizes.DeleteOnSubmit(product);
                    db.SubmitChanges();
                }
                catch
                {
                    TempData["errorDelete"] = "Xóa không thành công - có liên kết khóa ngoại!";
                    return RedirectToAction("SizeProduct");

                }
                return RedirectToAction("SizeProduct");
            }
        }
        public ActionResult ThemMauSP()
        {
            List<Product> product = db.Products.ToList();
            ViewBag.product = product;
            List<Size> sizes = db.Sizes.ToList();
            ViewBag.sizes = sizes;
            List<Size> Sizes = db.Sizes.ToList();
            ViewBag.Sizes = Sizes;
            return View();
        }

    }
}