using PagedList;
using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList; 

namespace QuanLyShopThoiTrang.Areas.Admin.Controllers
{
    public class QLDonHangController : Controller
    {
        // GET: Admin/QLDonHang
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QLDonHang( int ?page)
        {
            //var listProDuct = db.Products.ToList();
            var total = db.Orders.Sum(s => s.amount);
            ViewBag.total = total;
            List<Order> listProDuct = db.Orders.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1); // Trang mặc định là 1 nếu không có trang được chỉ định
            IPagedList<Order> pagedProducts = listProDuct.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);

        }
        public ActionResult OrderDetails(int id)
        {
            
            List<OrderDetail> listProDuct = db.OrderDetails.Where(s=>s.order_id==id).ToList();
            return View(listProDuct);
        }
    }
}