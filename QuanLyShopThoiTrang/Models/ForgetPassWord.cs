using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using static QuanLyShopThoiTrang.Models.AccountRegister;

namespace QuanLyShopThoiTrang.Models
{
    public class PhoneValidationAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Số điện thoại không hợp lệ (10 kí từ số).";

        public PhoneValidationAttribute() : base(DefaultErrorMessage) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are considered valid (you can change this behavior if needed)
            }

            var phoneNumber = value.ToString();

            // Check if the phone number is 10 digits long and starts with 0
            if (Regex.IsMatch(phoneNumber, @"^0\d{9}$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }

    public class ForgetPassWord
    {
        [Required(ErrorMessage = "Nhập số điện thoại vào đây!")]
        [PhoneValidation(ErrorMessage = "Số điện thoại không hợp lệ (10 kí từ số bắt đầu từ 0).")]
        public string DienThoai { get; set; }
    }

}