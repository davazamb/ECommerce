using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class TaxResponse
    {
        public int TaxId { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public  Company Company { get; set; }
        public  ICollection<Product> Products { get; set; }
    }
}