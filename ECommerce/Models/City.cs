using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Name { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int DepartamentId { get; set; }

        public virtual Departament Department { get; set; }
    }
}