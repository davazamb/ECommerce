using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class AddProductView
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Dese seleccionar un {0}")]
        [Display(Name = "Producto", Prompt = "[Seleccione un producto...]")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ingresar valores mayores que {1} y valor {0} ")]
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }

    }
}