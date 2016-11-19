using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class OrderDetailResponse
    {
        public int OrderDetailId { get; set; }   
        public string Description { get; set; }
        public double TaxRate { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public  Order Order { get; set; }
        public  Product Product { get; set; }
    }
}