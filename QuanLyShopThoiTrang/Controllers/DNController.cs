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
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=LAPTOP-IH11UO7O;Initial Catalog=QuanLyShop;Integrated Security=True");
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
        public ActionResult Register(string Ho,string Ten,string Address,string Email,string UserName,string DienThoai,string PassWord)
        {
            if (ModelState.IsValid)
            {
                Customer cus = new Customer();
                cus.Name = Ho + Ten;
                cus.Email = Email;
                cus.Address = Address;
                cus.Phone = DienThoai;
                db.Customers.InsertOnSubmit(cus);
                db.SubmitChanges();
                var cusnewID = db.Customers.OrderByDescending(s=>s.customer_id).FirstOrDefault().customer_id;
                Account account = new Account();
                account.customerId = cusnewID;
                account.status = 0;
                account.password = PassWord;
                account.username = UserName;
                db.Accounts.InsertOnSubmit(account);
                db.SubmitChanges();
            }
            return RedirectToAction("DangNhap");

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
                    Session["account"] = acc;
                    var accountTK = (from e in db.Accounts
                                     where e.username == acc.UserName
                                     where e.password == acc.Password
                                     select e).FirstOrDefault();
                    if (accountTK.status == 1)
                    {
                        return RedirectToAction("ProductPatial", "Product", new { area = "Admin"}); // chuyển hướng đến trang admin
                    }
                    else if (accountTK.status == 0)
                    {
                        return RedirectToAction("Index", "Home"); // chuyển hướng đến trang user
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
       
        public ActionResult DangXuat()
        {
            return RedirectToAction("DangNhap","DN");
        }

    }


}
