using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class CategoryRespon
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public Company Company { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}