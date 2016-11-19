using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class DepartmentResponse
    {
        public int DepartamentId { get; set; }
        public string Name { get; set; }
        public  ICollection<City> Cities { get; set; }
        public  ICollection<Customer> Customers { get; set; }
    }
}