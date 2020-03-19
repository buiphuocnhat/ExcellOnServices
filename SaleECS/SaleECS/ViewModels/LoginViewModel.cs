using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleECS.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Login Name", AllowEmptyStrings = false)]
        public string LoginName { get; set; }
        //[Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        //[DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        //[StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 6 characters.")]
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} is at least of {2} and maximum of {1} characters")]
        public string Password { get; set; }
    }
}