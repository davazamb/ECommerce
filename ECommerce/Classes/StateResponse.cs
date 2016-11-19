using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class StateResponse
    {
        public int StateId { get; set; }
        public string Description { get; set; }
        public  ICollection<Order> Orders { get; set; }
    }
}