using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.ViewModel
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public List<Product> Products { get; set; } // Thêm thuộc tính Products
    }
}