using ShoeStoreProject.Areas.Admin.Data;
using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Areas.Admin.Controllers
{
    public class QLOrdersController : Controller
    {
        // GET: Admin/QLOrders
        private ModelShoeStore context = new ModelShoeStore();
       //[Authorize(Roles = "Admin")]
        public ActionResult Orders()
        {
            //if (session["role"] == null || session["role"].tostring() != "customer")
            //{
            //    return redirecttoaction("login", "login");
            //}
            var item = from m in context.Orders
                       select new OrdersViewModel()
                       {
                           OrderID = m.OrderID,
                           CustomerID = m.CustomerID,
                           OrderDate = m.OrderDate,
                           TotalAmount = m.TotalAmount,
                           Status = m.Status,
                       };
            return View(item);
        }
        [HttpGet]
       // [Authorize(Roles = "Admin")]
        public ActionResult DetailsOrder (int? id)
        {
            var order = context.OrderDetails.Where(x => x.OrderID == id).ToList();
            if (order == null)
            {
                return RedirectToAction("Orders", "QLOrders");
            }
            return View(order);
          
        }
        public ActionResult Approve(int id)
        {
            var order = context.Orders.Find(id);
            if (order == null)
            {
                TempData["Message"] = "Order not found.";
                return RedirectToAction("Orders", "QLOrders");
            }

            try
            {
                order.Status = "Processing";
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
                TempData["Message"] = "Order approved successfully.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Failed to approve the order. Error: " + ex.Message;
            }

            return RedirectToAction("Orders", "QLOrders");
        }

        // GET: Admin/QLOrders/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var order = context.Orders
                               .Where(o => o.OrderID == id)
                               .Select(o => new OrdersViewModel
                               {
                                   OrderID = o.OrderID,
                                   CustomerID = o.CustomerID,
                                   OrderDate = o.OrderDate,
                                   Status = o.Status,
                                   TotalAmount = o.TotalAmount
                               })
                               .FirstOrDefault();

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }


        // POST: Admin/QLOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = context.Orders.Find(id);
            if (order == null)
            {
                TempData["Message"] = "Order not found.";
                return RedirectToAction("Orders", "QLOrders");
            }

            try
            {
                context.Orders.Remove(order);

                // Optional: Remove related order details
                var orderDetails = context.OrderDetails.Where(od => od.OrderID == id).ToList();
                foreach (var detail in orderDetails)
                {
                    context.OrderDetails.Remove(detail);
                }

                context.SaveChanges();
                TempData["Message"] = "Order deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Failed to delete the order. Error: " + ex.Message;
            }

            return RedirectToAction("Orders", "QLOrders");
        }
    }
}
