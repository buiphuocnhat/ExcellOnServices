using SaleECS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleECS.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} is at least of {2} and maximum of {1} characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy-MM-dd")]
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; } = "default.jpg";
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public int DepartmentId { get; set; }
        public string ResetPasswordCode { get; set; }


    }
}