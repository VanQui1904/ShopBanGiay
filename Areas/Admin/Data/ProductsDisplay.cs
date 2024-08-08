using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.Areas.Admin.Data
{
    public class ProductsDisplay
    {
        public int ProductID { get; set; }
        public string proName { get; set; }
        public string proImage {  get; set; }  
        public decimal? Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string BrandName { get; set; }
        public int? Stock { get; set; }
        public string CategoryName { get; set; }
    }
}