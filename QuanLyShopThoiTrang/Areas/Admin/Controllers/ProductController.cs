using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Linq;
using System.Web.UI.WebControls;
using System.IO;
using PagedList;
using System.Web.UI;

namespace QuanLyShopThoiTrang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductPatial(int? page)
        {
            //var listProDuct = db.Products.ToList();
            List<Product> listProDuct = db.Products.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
            IPagedList<Product> pagedProducts = listProDuct.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);

        }
        public ActionResult ThemSP()
        {

            List<Category> Cate = db.Categories.ToList();
            ViewBag.Cate = Cate;
            return View();
        }
        [HttpPost]
        public ActionResult ThemSP([Bind(Include = " product_name,Title,price,category_id,Dateadd")] Product p, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (p.category_id != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string directoryPath = Server.MapPath("~/Image/SanPham/");
                        string filePath = Path.Combine(directoryPath, fileName);

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        file.SaveAs(filePath);
                        p.ImageSP = fileName;
                    }
                   
                    else
                    {
                        p.ImageSP = "";
                    }    
                }
                db.Products.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            return RedirectToAction("ProductPatial");
        }
        public ActionResult SuaSP(int id)
        {
            Product product = db.Products.Where(row => row.product_id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult SuaSP(Product pr)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Where(row => row.product_id == pr.product_id).FirstOrDefault();
                //Update
                product.product_id = pr.product_id;
                product.Title = pr.Title;
                product.price = pr.price;
                product.ImageSP = pr.ImageSP;
                product.product_name = pr.product_name;
                product.Dateadd = pr.Dateadd;
                db.SubmitChanges();
            }
            return RedirectToAction("ProductPatial");
        }

        public ActionResult XoaSP(int id)
        {
            Product product = db.Products.Where(row => row.product_id == id).FirstOrDefault();
            return View(product);

        }
        [HttpPost]
        public ActionResult XoaSP(Product p)
        {
            Product product = db.Products.Where(row => row.product_id == p.product_id).FirstOrDefault();
            if (product == null)
            {
                //nếu xóa không thành công trả về giáo diện đang xóa hoặc giao diện nào z?
                ViewBag.ErrorDelete = "Xóa không thành công !";
                return RedirectToAction("ProductPatial");
            }
            else
            {
                try
                {
                    db.Products.DeleteOnSubmit(product);
                    db.SubmitChanges();
                }
                catch
                {
                    TempData["errorDelete"] = "Xóa không thành công - có liên kết khóa ngoại!";
                    return RedirectToAction("ProductPatial");

                }
                return RedirectToAction("ProductPatial");
            }
        }
        public ActionResult ThemMauSP()
        {
            List<Product> product =  db.Products.ToList();
            ViewBag.product = product;
            List<Size> sizes = db.Sizes.ToList();
            ViewBag.sizes = sizes;
            List<Color> colors = db.Colors.ToList();
            ViewBag.colors = colors;            
            return View();
        }
        [HttpPost]
        public ActionResult ThemMauSP([Bind(Include = "product_id, quantity, color_id, size_id")] ProductVariant p, HttpPostedFileBase file)
        {

                if (file != null && file.ContentLength > 0)
                {
                    
                    if (p.product_id != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string directoryPath = Server.MapPath("~/Image/SanPham/");
                        string filePath = Path.Combine(directoryPath, fileName);

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        file.SaveAs(filePath);
                        p.ImageSP = fileName;
                    }
                    else
                    {
                        p.ImageSP = "";
                    }

                db.ProductVariants.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            return RedirectToAction("ProductVariantPatial");
        }
        public ActionResult ProductVariantPatial(int? page)
        {
            //var listProDuct = db.Products.ToList();
            List<ProductVariant> listProDuct = db.ProductVariants.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
            IPagedList<ProductVariant> pagedProducts = listProDuct.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
           
        }
        public ActionResult SuaMauSP(int id) 
        {
            ProductVariant productvar = db.ProductVariants.Where(row => row.variant_id == id).FirstOrDefault();
            return View(productvar);
        }
        [HttpPost]
        public ActionResult SuaMauSP(ProductVariant pr)
        {
            if (ModelState.IsValid)
            {
                ProductVariant product = db.ProductVariants.Where(row => row.variant_id == pr.variant_id).FirstOrDefault();
                if (product != null)
                {
                    // Update
                    product.product_id = pr.product_id;
                    product.size_id = pr.size_id;
                    product.color_id = pr.color_id;

                    // Handle file upload
                    product.ImageSP = pr.ImageSP;

                    db.SubmitChanges();
                }
            }
            return RedirectToAction("ProductVariantPatial");
        }
        public ActionResult XoaMauSP(int id)
        {
            ProductVariant productvar = db.ProductVariants.Where(row => row.variant_id == id).FirstOrDefault();
            return View(productvar);
        }
        [HttpPost]
        public ActionResult XoaMauSP(ProductVariant product)
        {
            ProductVariant variant = db.ProductVariants.Where(row => row.variant_id == product.variant_id).FirstOrDefault();
            if (product == null)
            {
                //nếu xóa không thành công trả về giáo diện đang xóa hoặc giao diện nào z?
                ViewBag.ErrorDelete = "Xóa không thành công !";
                return RedirectToAction("ProductPatial");
            }
            else
            {
                try
                {
                    db.ProductVariants.DeleteOnSubmit(variant);
                    db.SubmitChanges();
                }
                catch
                {
                    TempData["errorDelete"] = "Xóa không thành công - có liên kết khóa ngoại!";
                    return RedirectToAction("ProductPatial");

                }
                return RedirectToAction("ProductPatial");
            }
        }

    }
}