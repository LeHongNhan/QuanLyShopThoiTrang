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
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PartialNEWARRIVALS(int skip = 0, int take = 8)
        {
            var latestProducts = db.Products
                                     .Skip(skip)
                                     .Take(take)
                                     .ToList();
            return View( latestProducts);
        }

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

        public ActionResult BestSeller()
        {
            var listBest = db.Products.Take(10).ToList();

            return View(listBest);
        }

    }
}