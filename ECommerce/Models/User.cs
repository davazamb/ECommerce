using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(256, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "E-mail")]
        [Index("User_Name_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Estado")]
        public int DepartamentId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }

        [Display(Name = "Usuario")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        [NotMapped]
        [Display(Name = "Foto")]
        public HttpPostedFileBase PhotoFile { get; set; }
        //Esta es lado de varios de la relacion su Padre
        [JsonIgnore]
        public virtual Departament Departament { get; set; }
        [JsonIgnore]
        public virtual City City { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }
    }
}