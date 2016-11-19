using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class CityResponse
    {
        public int CityId { get; set; }
        public string Name { get; set; }        
        public  Departament Departament { get; set; }
        public  ICollection<Customer> Customers { get; set; }
    }
}