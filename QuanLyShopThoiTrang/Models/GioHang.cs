using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace QuanLyShopThoiTrang.Models
{
    public class GioHang
    {
        QuanLyTheCIU1DataContext db = new QuanLyTheCIU1DataContext(@"Data Source=MSI\CONG03;Initial Catalog=QuanLyShop;Integrated Security=True");
        public string iTensp { get; set; }
        public string iImage { get; set; }
        public double dPrice { get; set; }
        public int iSoluong { get; set; }
         public int iProductid {  get; set; }

        public double dToltalPrice
        {
            get { return iSoluong * dPrice; }
        }
        public GioHang(int id)
        {
            this.iProductid = id;
            Product p = db.Products.Single(s => s.product_id == id);
            this.iTensp = p.product_name;
            this.iImage = p.ImageSP;
            this.iSoluong = 1;
            this.dPrice = double.Parse(p.price.ToString());
        }
    }
}