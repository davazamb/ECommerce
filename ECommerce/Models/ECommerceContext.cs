using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext() : base("DefaultConnection")
        {

        }

        public DbSet<Departament> Departaments { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.City> Cities { get; set; }
    }
}
