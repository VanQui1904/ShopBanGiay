﻿using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.Areas.Admin.Data
{
    public class OrderDetailViewModel
    {
        public int OrderDetailID { get; set; }

        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }
        public virtual Order Order { get; set; }
    }
}