using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuanLyShopThoiTrang.Models
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CustomEmailValidationAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Định dạng email không hợp lệ.";

        public CustomEmailValidationAttribute() : base(DefaultErrorMessage) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success; // Null or empty values are considered valid
            }

            var email = value.ToString();

            // Implement your custom email format validation logic using regex or other methods
            if (!IsCustomEmailValid(email))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        private bool IsCustomEmailValid(string email)
        {
            // Implement your custom email format validation logic using regex or other methods
            // For example, you can use a regex pattern
            const string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }



    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MustMatchAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Mật khẩu và Nhập lại mật khẩu không trùng khớp.";

        public MustMatchAttribute() : base(DefaultErrorMessage) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (AccountRegister)validationContext.ObjectInstance;

            if (model.PassWord != model.RePassWord)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }


    public class AccountRegister
    {
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
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
        [Required(ErrorMessage = "Nhập họ vào đây!")]
        public string Ho { get; set; }

        [Required(ErrorMessage = "Nhập tên vào đây!")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Nhập email vào đây!")]
        [CustomEmailValidation(ErrorMessage = "Định dạng email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại vào đây!")]
        [PhoneValidation(ErrorMessage = "Số điện thoại không hợp lệ (10 kí từ số).")]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu vào đây!")]
        [MustMatch(ErrorMessage = "Mật khẩu và Nhập lại mật khẩu không trùng khớp.")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu vào đây!")]
        [MustMatch(ErrorMessage = "Mật khẩu và Nhập lại mật khẩu không trùng khớp.")]
        public string RePassWord { get; set; }
    }

}
