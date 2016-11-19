using Newtonsoft.Json;
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
        [Display(Name = "Telefono")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }
        [NotMapped]
        [Display(Name = "Logo")]
        public HttpPostedFileBase LogoFile { get; set; }

        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Estado")]
        public int DepartamentId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }

        //Esta es lado de varios de la relacion su Padre
        [JsonIgnore]
        public virtual Departament Departament { get; set; }
        [JsonIgnore]
        public virtual City City { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
        [JsonIgnore]
        public virtual ICollection<Category> Categories { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tax> Taxes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyCustomer> CompanyCustomers { get; set; }
        [JsonIgnore]
        public virtual ICollection<WareHouse> WareHouses { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}