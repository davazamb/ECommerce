using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class InventoryResponse
    {
        public int InventoryId { get; set; }
        public double Stock { get; set; }
        public  WareHouse WareHouse { get; set; }
        public  Product Product { get; set; }
    }
}