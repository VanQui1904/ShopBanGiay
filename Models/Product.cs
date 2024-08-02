namespace ShoeStoreProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
