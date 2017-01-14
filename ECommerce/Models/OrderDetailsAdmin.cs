using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class OrderDetailsAdmin
    {                             
        public int OrderId { get; set; }  
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe selecionar una {0}")]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Estatus")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string Remarks { get; set; }
        public List<OrderDetail> ODetails { get; set; }
        public virtual Customer Customer { get; set; }  
        public virtual State State { get; set; }    
        public virtual Company Company { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}