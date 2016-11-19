using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Photo { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int DepartamentId { get; set; }

        public string DepartamentName { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public Company Company { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsUser { get; set; }

        public bool IsCustomer { get; set; }

        public bool IsSupplier { get; set; }
    }

}