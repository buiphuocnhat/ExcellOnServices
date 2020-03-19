using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleECS.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    }
}