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
        public ActionResult Orders()
        {
            var item = from m in context.Orders
                       select new OrdersViewModel()
                       {
                           OrderID = m.OrderID,
                           CustomerID = m.CustomerID,
                           OrderDate = m.OrderDate,
                           TotalAmount = m.TotalAmount,
                       };
            return View(item);
        }
        [HttpGet]
        public ActionResult DetailsOrder (int? id)
        {
            var order = context.OrderDetails.Where(x => x.OrderID == id).ToList();
            if (order == null)
            {
                return RedirectToAction("Orders", "QLOrders");
            }
            return View(order);
          
        }
    }
}
