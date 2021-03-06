//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaleECS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderServiceDetail
    {
        public int Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        public int OrderServiceId { get; set; }
        public int ServiceId { get; set; }
        public bool Status { get; set; }
    
        public virtual OrderService OrderService { get; set; }
        public virtual Service Service { get; set; }
    }
}
