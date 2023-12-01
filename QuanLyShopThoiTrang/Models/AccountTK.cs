using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyShopThoiTrang.Models
{
    public class AccountTK
    {
        [Required(ErrorMessage = "Nhập tên tài khoản vào đây!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu vào đây!")]
        public string Password { get; set; }
        public string Role { get; set; } // Thêm thuộc tính Role để lưu vai trò của người dùng
    }

}