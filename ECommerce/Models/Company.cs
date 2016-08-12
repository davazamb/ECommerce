using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Empresa")]
        [Index("Company_Name_Index", IsUnique = true)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        public string Address { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }
        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int DepartamentId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int CityId { get; set; }

        //Esta es lado de varios de la relacion su Padre
        public virtual Departament Departament { get; set; }
        public virtual City City { get; set; }
    }
}