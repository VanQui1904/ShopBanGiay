using ShoeStoreProject.Areas.Admin.Data;
using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Areas.Admin.Controllers
{
    public class QLCustomersController : Controller
    {
        // GET: Admin/QLCustomers
        private ModelShoeStore context = new ModelShoeStore();
        public ActionResult Customers()
        {
            var itemCus = from c in context.Customers
                          select new CustomersViewModel()
                          {
                              CustomerID = c.CustomerID,
                              FirstName = c.FirstName,
                              LastName = c.LastName,
                              Email = c.Email,
                              Phone = c.Phone,
                              Address = c.Address,
                              City = c.City,
                          };
            return View(itemCus);
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomersViewModel forndata)
        {
            var itemcus = new Customer();
            itemcus.FirstName = forndata.FirstName;
            itemcus.LastName = forndata.LastName;
            itemcus.Email = forndata.Email;
            itemcus.Phone = forndata.Phone;
            itemcus.Address = forndata.Address;
            itemcus.City = forndata.City;
            context.Customers.Add(itemcus);
            context.SaveChanges();
            return View(itemcus);
        }
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            var editCus = (from  c in context.Customers
                           where c.CustomerID == id
                           select new CustomersViewModel
                           {
                               FirstName = c.FirstName,
                               LastName = c.LastName,
                               Email = c.Email,
                               Phone = c.Phone,
                               Address = c.Address,
                               City = c.City,
                               Country = c.Country,

                           }).FirstOrDefault();
            if (editCus == null)
            {
                return RedirectToAction("Customers", "QLCustomers");
            }
            return View(editCus);
        }
        [HttpPost]
        public ActionResult EditCustomer(CustomersViewModel formdata)
        {
            var editcus = context.Customers.Where(x=>x.CustomerID == formdata.CustomerID).FirstOrDefault();
            if (editcus == null)
            {
                return RedirectToAction("Customers", "QLCustomers");
            }
            editcus.FirstName = formdata.FirstName;
            editcus.LastName = formdata.LastName;
            editcus.Email = formdata.Email;
            editcus.Phone = formdata.Phone;
            editcus.Address = formdata.Address;
            editcus.City = formdata.City;
            editcus.Country = formdata.Country;
            context.SaveChanges();
            return RedirectToAction("Customers", "QLCustomers");
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            var delCus = context.Customers.Where(d => d.CustomerID == id).FirstOrDefault();
            if(delCus == null)
            {
                return RedirectToAction("Customers", "QLCustomers");
            }
            context.Customers.Remove(delCus);
            context.SaveChanges();
            return View(delCus);
        }
    }
}