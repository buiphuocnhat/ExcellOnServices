using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleECS.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Status { get; set; }

        [Required]
        public Nullable<int> CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}