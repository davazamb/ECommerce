using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class WareHouse
    {
        [Key]
        public int WareHouseId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Index("WareHouse_CompanyId_Name_Index", 1, IsUnique = true)]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Bodega")]
        [Index("WareHouse_CompanyId_Name_Index", 2, IsUnique = true)]
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
       
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Estado")]
        public int DepartamentId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }

        [JsonIgnore]
        public virtual Departament Departament { get; set; }
        [JsonIgnore]
        public virtual City City { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }

        [JsonIgnore]
        public virtual ICollection<Inventory> Inventories { get; set; }

    }
}