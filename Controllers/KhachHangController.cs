using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoeStoreProject.Models;
namespace ShoeStoreProject.Controllers
{
    public class KhachhangController : Controller
    {
        private ModelShoeStore _db = new ModelShoeStore();
        // GET: Khachhang
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer cus)
        {
            _db.Customers.Add(cus);
            _db.SaveChanges();
            return RedirectToAction("ShowToCart", "Cart");
        }
    }
}