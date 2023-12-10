using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Areas.Admin.Controllers
{
    public class ColorController : Controller
    {
        // GET: Admin/Color
        // GET: Admin/CateProduct
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");

        public ActionResult ColorProduct()
        {
            var list = db.Colors.ToList();
            return View(list);

        }
        public ActionResult AddColor()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AddColor([Bind(Include = "color_name")] Color p)
        {
            if (ModelState.IsValid)
            {
                db.Colors.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            return RedirectToAction("ColorProduct");
        }
        public ActionResult UpdateColor(int id)
        {
            Color product = db.Colors.Where(row => row.color_id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult UpdateColor(Color pr)
        {
            if (ModelState.IsValid)
            {
                Color product = db.Colors.Where(row => row.color_id == pr.color_id).FirstOrDefault();
                //Update

                product.color_name = pr.color_name;

                db.SubmitChanges();
            }
            return RedirectToAction("ColorProduct");
        }

        public ActionResult DeleteColor(int id)
        {
            Color product = db.Colors.Where(row => row.color_id == id).FirstOrDefault();
            return View(product);

        }
        [HttpPost]
        public ActionResult DeleteColor(Color p)
        {
            Color product = db.Colors.Where(row => row.color_id == p.color_id).FirstOrDefault();
            if (product == null)
            {
                //nếu xóa không thành công trả về giáo diện đang xóa hoặc giao diện nào z?
                ViewBag.ErrorDelete = "Xóa không thành công !";
                return RedirectToAction("ColorProduct");
            }
            else
            {
                try
                {
                    db.Colors.DeleteOnSubmit(product);
                    db.SubmitChanges();
                }
                catch
                {
                    TempData["errorDelete"] = "Xóa không thành công - có liên kết khóa ngoại!";
                    return RedirectToAction("ColorProduct");

                }
                return RedirectToAction("ColorProduct");
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