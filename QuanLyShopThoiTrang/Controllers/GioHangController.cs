using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemVaoGioHang(int ms, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanPham = lstGioHang.Find(sp => sp.iProductid == ms);
            if (sanPham == null)
            {
                sanPham = new GioHang(ms);
                lstGioHang.Add(sanPham);
                return Redirect(strURL);
            }
            else
            {
                sanPham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                tsl = lst.Sum(sp => sp.iSoluong);
            }
            return tsl;
        }
        private double TongThahTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                ttt += lstGioHang.Sum(sp => sp.dToltalPrice);
            }
            return ttt;
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTT = TongThahTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TongSoLuong();
            return View();
        }
        public ActionResult XoaGioHang(int id)
        {
            var GioHang = LayGioHang();
            GioHang sp = GioHang.Single(s => s.iProductid == id);
            if (sp == null)
                return HttpNotFound();
            GioHang.RemoveAll(s => s.iProductid == id);
            return RedirectToAction("GioHang", "GioHang");
        }
        
        public ActionResult CapNhatGioHang(int id, FormCollection f)
        {
            var GioHang = LayGioHang();
            GioHang sp = GioHang.Single(s => s.iProductid == id);
            if (sp == null)
                return HttpNotFound();
            sp.iSoluong = int.Parse(f["txt_SoLuong"].ToString());
            return RedirectToAction("GioHang", "GioHang");
        }
        public ActionResult Dathang(FormCollection f)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dathang( )
        {
            if (Session["account"] == null)
            {
                return RedirectToAction("DangNhap", "DN");
            }
            try
            {
                var Order = new Order();
                Order.order_date = DateTime.Now;
               AccountTK acc = Session["account"] as AccountTK;
                var cusID = (from e in db.Accounts
                             where e.username == acc.UserName
                             where e.password == acc.Password
                             select e).FirstOrDefault().customerId;
                Order.customer_id = cusID;
                Order.status = 0;
                Order.amount = TongThahTien();
                db.Orders.InsertOnSubmit(Order);
                db.SubmitChanges();
                var newid = db.Orders.OrderByDescending(s => s.order_id).First().order_id;
                if (Session["GioHang"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                List<GioHang> lstGioHang = LayGioHang();
                foreach (var item in lstGioHang)
                {
                    var varri = db.ProductVariants.Where(s => s.product_id == item.iProductid).FirstOrDefault().variant_id;
                    OrderDetail detail = new OrderDetail();
                    detail.order_id = newid;
                    detail.variant_id = varri;
                    detail.quantity = item.iSoluong;
                    detail.subtotal = item.dToltalPrice;
                    db.OrderDetails.InsertOnSubmit(detail);
                    db.SubmitChanges();
                }
                Session["GioHang"] = null;
                return RedirectToAction("Index", "Home");
               
            }
           catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult MuaNgay(int ms)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanPham = lstGioHang.Find(sp => sp.iProductid == ms);
            if (sanPham == null)
            {
                sanPham = new GioHang(ms);
                lstGioHang.Add(sanPham);
                return RedirectToAction("GioHang");
            }
            else
            {
                sanPham.iSoluong++;
                return RedirectToAction("GioHang");
            }
        }
    }
}