using System;
using System.Linq;
using System.Web.Mvc;
using ShoeStoreProject.Models;
using ShoeStoreProject.ViewModel;
using ShoeStoreProject.Areas.Admin.Data;


namespace ShoeStoreProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ModelShoeStore _db;

        public CartController()
        {
            _db = new ModelShoeStore();
        }

        // Lấy giỏ hàng từ session hoặc tạo mới nếu không tồn tại
        private Cart GetCart()
        {
            var cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Thêm sản phẩm vào giỏ hàng
        public ActionResult AddToCart(int id)
        {
            
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Login"); // Chuyển hướng tới trang đăng nhập nếu người dùng chưa đăng nhập
            //}
            var product = _db.Products.SingleOrDefault(p => p.ProductID == id);
            if (product != null)
            {
                GetCart().Add(product);
            }
            return RedirectToAction("ShowToCart","Cart");
        }

        // Hiển thị nội dung giỏ hàng
        public ActionResult ShowToCart()
        {
            var cart = GetCart();
            return View(cart);
        }

        // Cập nhật số lượng sản phẩm trong giỏ hàng
        [HttpPost]
        public ActionResult Update_Quantity_Cart(int id_pro, int quantity)
        {
            var cart = GetCart();
            if (quantity > 0)
            {
                cart.Update_Quantity_Shopping(id_pro, quantity);
            }
            return RedirectToAction("ShowToCart");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public ActionResult RemoveCart(int id)
        {
            var cart = GetCart();
            cart.Remove_Cart(id);
            return RedirectToAction("ShowToCart");
        }

        // Hiển thị phần tổng quan giỏ hàng
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
        //checkout
        public ActionResult CheckOut(FormCollection form)
        {
            if (Session["Login"] == null || (bool)Session["Login"] == false)
            {
                return RedirectToAction("Login", "Login");
            }

            var customerID = Session["CustomerID"];
            if (customerID == null)
            {
                // Log thông báo hoặc thực hiện hành động phù hợp nếu CustomerID không tồn tại trong Session
                return RedirectToAction("Login", "Login");
            }

            try
            {
                Cart cart = Session["Cart"] as Cart;

                decimal totalAmount = cart.Items.Sum(item => item._shopping_product.Price.Value * item._shopping_quantity);
                Order _orders = new Order
                {
                    CustomerID = (int)customerID,  // Chuyển đổi và sử dụng CustomerID từ Session
                    OrderDate = DateTime.Now,
                    OrderPhone = form["Phone"],
                    AddressOrder = form["Diachigiaohang"],
                    TotalAmount = totalAmount,
                };

                _db.Orders.Add(_orders);
                foreach (var item in cart.Items)
                {
                    OrderDetail _orderDetails = new OrderDetail
                    {
                        OrderID = _orders.OrderID,
                        ProductID = item._shopping_product.ProductID,
                        Quantity = item._shopping_quantity,
                        UnitPrice = item._shopping_product.Price,
                    };
                    _db.OrderDetails.Add(_orderDetails);
                }

                _db.SaveChanges();
                cart.ClearCart();

                TempData["OrderId"] = _orders.OrderID;
                return RedirectToAction("Shopping_Success", "Cart");
            }
            catch
            {
                return Content("Lỗi! Vui lòng điền đầy đủ thông tin!");
            }
        }

        public ActionResult OrderDetails(int orderId)
        {
            var order = _db.Orders.SingleOrDefault(o => o.OrderID == orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            var orderDetails = _db.OrderDetails.Where(od => od.OrderID == orderId).ToList();
            var products = _db.Products.ToList();

            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                OrderDetails = orderDetails,
                Products  = products // Truyền danh sách sản phẩm vào mô hình
            };

            return View(viewModel);
        }


    }

}

