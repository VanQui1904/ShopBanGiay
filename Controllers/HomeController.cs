using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private ModelShoeStore context = new ModelShoeStore();
        public ActionResult Index()
        {
            var item = context.Products.ToList();
            return View(item);
        }
        public ActionResult DetailGiay(int id)
        {
            var giay = from m in context.Products.Where(m=>m.ProductID == id) select m;
            if (giay == null)
            {
                ViewBag.message = "Sản phẩm đã hết hàng";
                return RedirectToAction("Index", "QLGiay");
            }
            return View(giay);

        }
    }
}