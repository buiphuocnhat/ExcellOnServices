using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleECS.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<bool> Gender { get;set ; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}