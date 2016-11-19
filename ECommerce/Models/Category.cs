using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Categoria")]
        [Index("Category_CompanyId_Description_Index", 2, IsUnique = true)]
        public string Description { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Index("Category_CompanyId_Description_Index", 1, IsUnique = true)]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

    }
}