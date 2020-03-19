using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleECS.ViewModels
{
    public class OrderServiceViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<System.DateTime> PaymentLate { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeName { get; set; }
    }
}