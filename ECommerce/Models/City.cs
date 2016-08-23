using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Ciudad")]
        [Index("City_Departament_Name_Index", 2, IsUnique = true)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Index("City_Departament_Name_Index", 1, IsUnique = true)]
        [Display(Name = "Estado")]
        public int DepartamentId { get; set; }

        //Su padre
        public virtual Departament Department { get; set; }

        //EL Ciudad tiene varias y se pluraliza su hijo
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<WareHouse> WareHouses { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}