using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Remarks { get; set; }
        //Campo de solo lectura de calculo
        //TODO:Error de valor nulo

        public double Stock { get; set; }
        //public Inventory Inventory { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public Company Company { get; set; }
        public Category Category { get; set; }
        public Tax Tax { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }        

    }
}