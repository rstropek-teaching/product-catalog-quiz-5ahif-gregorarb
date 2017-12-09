using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product's Name")]
        [StringLength(70)]
        public string Name { get; set; }

        [Display(Name = "Product's Description")]
        [StringLength(70)]
        public string Description { get; set; }

        [Display(Name = "Product's Price per Unit")]
        public int UnitPrice { get; set; }
    }
}
