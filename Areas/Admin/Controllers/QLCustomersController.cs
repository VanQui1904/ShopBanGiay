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
        //[Authorize(Roles = "Admin")]
        public ActionResult Customers()
        {
            //if (Session["Role"] == null || Session["Role"].ToString() != "Customer")
            //{
            //    return RedirectToAction("Login", "Login");
            //}
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
        //[Authorize(Roles = "Admin")]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
       //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "Admin")]
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
            if (delCus == null)
            {
                return RedirectToAction("Customers", "QLCustomers");
            }
            return View(delCus);
        }

        // POST: QLCustomers/DeleteCustomer/5
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var delCus = context.Customers.Where(d => d.CustomerID == id).FirstOrDefault();
            if (delCus == null)
            {
                return RedirectToAction("Customers", "QLCustomers");
            }

            try
            {
                context.Customers.Remove(delCus);
                context.SaveChanges();
                TempData["SuccessMessage"] = "Khách hàng đã được xóa thành công.";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    TempData["ErrorMessage"] = "Không thể xóa khách hàng này vì đang có dữ liệu liên quan.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa khách hàng.";
                }
            }

            return RedirectToAction("Customers", "QLCustomers");
        }

    }
}