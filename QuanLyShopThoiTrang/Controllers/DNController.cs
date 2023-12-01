using QuanLyShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyShopThoiTrang.Controllers
{
    public class DNController : Controller
    {
        // GET: DN
        // GET: DN
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True;");
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountRegister ac)
        {
            if (ModelState.IsValid)
            {
                // xử lý nếu có dữ liệu.
            }
            return View(ac);

        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(AccountTK acc)
        {
            if (ModelState.IsValid)
            {
                int count = db.Accounts.Where(ac => ac.username == acc.UserName && ac.password == acc.Password).Count();
                if (count > 0)
                {
                    var accountTK = (from e in db.Accounts
                                     where e.username == acc.UserName
                                     where e.password == acc.Password
                                     select e).FirstOrDefault();
                    if (accountTK.status == 1)
                    {
                        return RedirectToAction("AdminDashboard", "Home"); // chuyển hướng đến trang admin
                    }
                    else if (accountTK.status == 0)
                    {
                        return RedirectToAction("Index", "Home"); // chuyển hướng đến trang user
                    }
                    else
                    {
                        // Không có cái quyền gì ở đây.
                    }
                }
                else
                {
                    ViewBag.ErrorLogin = "Tài khoản không tồn tại !";
                    return View(acc);
                }
            }

            return View(acc);
        }


        public ActionResult QuenMatKhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuenMatKhau(ForgetPassWord fpw)
        {
            if (ModelState.IsValid)
            {
                // xử lý nếu có dữ liệu.
            }
            return View(fpw);
        }

    }


}
