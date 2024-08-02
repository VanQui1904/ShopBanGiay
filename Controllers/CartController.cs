using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public class GiohangController : Controller
        {
            // GET: Giohang
            ModelShoeStore _db = new ModelShoeStore();

            public Cart GetCart()
            {
                Cart cart = Session["Cart"] as Cart;
                if (cart != null || Session["Cart"] == null)
                {

                    cart = new Cart();
                    Session["Cart"] = cart;


                }
                return cart;
            }

            public ActionResult AddToCart(int id)
            {
                var pro = _db.Products.SingleOrDefault(s => s.ProductID == id);
                if (pro != null)
                {

                    GetCart().Add(pro);
                }
                return RedirectToAction("ShowToCart", "Giohang");
            }
            public ActionResult ShowToCart()
            {
                if (Session["Cart"] == null)
                    return RedirectToAction("ShowToCart", "Giohang");
                Cart cart = Session["Cart"] as Cart;
                return View(cart);

            }
            public ActionResult Update_Quantity_Cart(FormCollection form)
            {
                Cart cart = Session["Cart"] as Cart;
                int id_pro = int.Parse(form["ID_Product"]);
                int quantity = int.Parse(form["Quantity"]);
                cart.Update_Quantity_Shopping(id_pro, quantity);
                return RedirectToAction("ShowToCart", "Giohang");
            }
            public ActionResult RemoveCart(int id)
            {
                Cart cart = Session["Cart"] as Cart;
                cart.Remove_Cart(id);
                return RedirectToAction("ShowToCart", "Giohang");
            }
            public PartialViewResult BagCart()
            {
                int total_item = 0;
                Cart cart = Session["Cart"] as Cart;
                if (cart != null)

                    total_item = cart.Total_Quantity_in_cart();
                ViewBag.QuantityCart = total_item;
                return PartialView("BagCart");
            }
            public ActionResult Shopping_Success()
            {
                return View();
            }
            //dat hang
            public ActionResult Checkout(FormCollection form)
            {
                //Customers _customers = new Customers();
                try
                {
                    Cart cart = Session["Cart"] as Cart;
                    Order _orders = new Order();

                    _orders.OrderDate = DateTime.Now;
                    _orders.TotalAmount = _orders.CustomerID;
                    _orders.OrderPhone = form["Phone"];
                    //_orders.CustomerID = @Html.HiddenFor(model => model.CustomerID);

                    _orders.AddressOrder = form["Diachigiaohang"];
                    _db.Orders.Add(_orders);
                    //{
                    //    Customers _customers = new Customers();
                    //    _customers.CustomerID = 

                    //}
                    foreach (var item in cart.Items)
                    {

                        OrderDetail _oders_detail = new OrderDetail();
                        
                        _oders_detail.OrderID = _orders.OrderID;
                        _oders_detail.ProductID = item._shopping_product.ProductID;
                        _oders_detail.UnitPrice = item._shopping_product.Price;
                        _oders_detail.Quantity = item._shopping_quantity;
                        






                        _db.OrderDetails.Add(_oders_detail);
                    }
                    _db.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("Shopping_Success", "Giohang");
                }
                catch
                {
                    return Content("Lỗi. Vui lòng điền đầy đủ thông tin..");


                }
            }
        }
    }
}
}