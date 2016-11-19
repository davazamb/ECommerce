using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Orden")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The filed {0} must be maximun {1} characters length")]
        [Display(Name = "Producto")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0, double.MaxValue, ErrorMessage = "The {0} must be between {1} and {2}")]
        [Display(Name = "Impuesto")]
        public double TaxRate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "You must enter values in {0} between {1} and {2}")]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "You must enter values in {0} between {1} and {2}")]
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

    }
}