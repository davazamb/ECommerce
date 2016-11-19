using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe selecionar una {0}")]
        [Index("Product_CompanyId_Description_Index", 1, IsUnique = true)]
        [Index("Product_CompanyId_BarCode_Index", 1, IsUnique = true)]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Producto")]
        [Index("Product_CompanyId_Description_Index", 2, IsUnique = true)]
        public string Description { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [MaxLength(13, ErrorMessage = "El Campo {0} debe tener maximo {1} Caracter de longitud")]
        [Display(Name = "Codigo Barra")]
        [Index("Product_CompanyId_BarCode_Index", 2, IsUnique = true)]
        public string BarCode { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Impuesto")]
        public int TaxId { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:C2}")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe seleccionar una {0} entre {1} y {2}")]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImageFile { get; set; }
        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
        //Campo de solo lectura de calculo
        //TODO:Error de valor nulo
        [JsonIgnore]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N2}")]
        public double Stock { get { return Inventories.Sum(i => i.Stock); } }
        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Tax Tax { get; set; }

        [JsonIgnore]
        public virtual ICollection<Inventory> Inventories { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetailTmp> OrderDetailTmps { get; set; }

    }
}