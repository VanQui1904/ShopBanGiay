using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.Areas.Admin.Data
{
    public class ProductsViewModel
    {
        public int ProductID { get; set; }

        [StringLength(100)]
        public string proName { get; set; }
        public string proImage { get; set; }

        public int? BrandID { get; set; }

        public int? CategoryID { get; set; }
        

        [StringLength(10)]
        public string Size { get; set; }

        [StringLength(20)]
        public string Color { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        [Column(TypeName = "text")]
        public string proDescription { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}