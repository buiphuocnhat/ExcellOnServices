using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleECS.ViewModels
{
    public class OrderServiceDetailViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public int OrderServiceId { get; set; }
        public int ServiceId { get; set; }
        public bool Status { get; set; }

        public string Code { get; set; }
        public string ServiceName{get; set;}
    }
}