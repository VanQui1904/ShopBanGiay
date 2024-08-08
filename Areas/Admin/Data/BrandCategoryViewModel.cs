using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.Areas.Admin.Data
{
    public class BrandCategoryViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public string SearchTerm { get; set; }
    }
}