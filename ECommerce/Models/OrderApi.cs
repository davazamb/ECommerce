using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class OrderApi
    {
        public int OrderId { get; set; }        
        public DateTime Date { get; set; }        
        public string Remarks { get; set; }
        public  Customer Customer { get; set; }
        public  State State { get; set; }
        public  Company Company { get; set; }        
        public  ICollection<OrderDetail> OrderDetails { get; set; }
    }
}