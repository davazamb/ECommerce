using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class CompanyCustomerResponse
    {
        public int CompanyCustomerId { get; set; }
        public  Company Company { get; set; }
        public  Customer Customer { get; set; }
    }
}