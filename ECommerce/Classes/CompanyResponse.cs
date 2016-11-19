using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class CompanyResponse
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        //Esta es lado de varios de la relacion su Padre
        public  Departament Departament { get; set; }
        public  City City { get; set; }
        public  ICollection<User> Users { get; set; }
        public  ICollection<Product> Products { get; set; }
        public  ICollection<CompanyCustomer> CompanyCustomers { get; set; }
    }
}