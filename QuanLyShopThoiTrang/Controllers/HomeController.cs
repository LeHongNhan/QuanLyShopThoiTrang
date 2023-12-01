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
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=MSI\CONG03;Initial Catalog=QuanLyShop;Integrated Security=True");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PatialNEWARRIVALS()
        {         
            var latestProducts = db.Products
                .OrderByDescending(p => p.product_id)
                .Take(16)
                .ToList();
            return View( latestProducts);
        }
        //public ActionResult _PatialNEWARRIVALS(int skipCount = 0)
        //{
        //    int takeCount = 16;
        //    var latestProducts = db.Products
        //        .OrderByDescending(p => p.product_id)
        //        .Skip(skipCount)
        //        .Take(takeCount)
        //        .ToList();
        //    return PartialView("_PartialNEWARRIVALS", latestProducts);
        //}
        public ActionResult _PatialOutstanding()
        {
            var latestProducts = db.Products.GroupBy(p => p.category_id)
            .Select(group => group.FirstOrDefault())  // Giả sử bạn có một thuộc tính DateAdded hoặc thuộc tính khác để xác định sản phẩm mới nhất
            .Take(9)
            .ToList();
            return View(latestProducts);

        }
        public ActionResult AdminDashboard()
        {
            //Làm lại trang admin và usêr
            return View();

        }


    }
}