using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.Areas.Admin.Data
{
    public class RevenueByDateViewModel
    {
        public DateTime Date { get; set; }
        public decimal? TotalRevenue { get; set; }
    }
}