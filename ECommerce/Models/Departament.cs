using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Departament
    {
        [Key]
        public int DepartamentId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Estado")]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
