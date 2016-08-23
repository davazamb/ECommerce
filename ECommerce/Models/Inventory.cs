using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        [Required]
        public int WareHouseId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N2}")]
        public double Stock { get; set; }

        public virtual WareHouse WareHouse { get; set; }
        public virtual Product Product { get; set; }
    }
}