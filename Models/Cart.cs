using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.Models
{
    public class CarItem
    {
        public Product _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }

    }
    public class Cart
    {
        List<CarItem> items = new List<CarItem>();
        public IEnumerable<CarItem> Items
        {
            get { return items; }

        }
        public void Add(Product _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.ProductID == _pro.ProductID);
            if (item == null)
            {
                items.Add(new CarItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity

                });
            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }
        public void Update_Quantity_Shopping(int id, int _quatity)
        {
            var item = items.Find(s => s._shopping_product.ProductID == id);
            if (item != null)
            {
                item._shopping_quantity = _quatity;
            }
        }
        public double Total_Money()
        {
            var total = items.Sum(s => s._shopping_product.Price * s._shopping_quantity);
            return (double)total;


        }
        public void Remove_Cart(int id)
        {
            items.RemoveAll(s => s._shopping_product.ProductID == id);
        }
        public int Total_Quantity_in_cart()
        {
            return items.Sum(s => s._shopping_quantity);

        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}