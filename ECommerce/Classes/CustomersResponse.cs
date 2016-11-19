using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class CustomersResponse
    {
        public int CustomerId { get; set; }
        
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Phone { get; set; }        
        public string Address { get; set; }       
        
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        
        public  Departament Departament { get; set; }
        
        public  City City { get; set; }
        
        public  ICollection<CompanyCustomer> CompanyCustomers { get; set; }
        
        public  ICollection<Order> Orders { get; set; }
    }
}