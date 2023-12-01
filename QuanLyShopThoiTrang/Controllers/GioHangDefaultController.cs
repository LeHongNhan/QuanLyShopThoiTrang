using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Controllers
{
    public class GioHangDefaultController : Controller
    {
        // GET: GioHangDefault
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=MSI\CONG03;Initial Catalog=QuanLyShop;Integrated Security=True");
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
                return RedirectToAction("Index1", "Home");
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
        public ActionResult XoaAll()
        {
            var GioHang = LayGioHang();
            GioHang.Clear();
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

    }
}