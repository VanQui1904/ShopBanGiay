using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        private ModelShoeStore context = new ModelShoeStore();

        // GET: Profile/Edit
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditProfile(int? id)
        {
            //if (Session["Role"] == null || Session["Role"].ToString() != "Customer")
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var model = new Customer
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                City = customer.City,
                Country = customer.Country,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email
            };

            return View(model);
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditProfile(Customer model)
        {
            if (ModelState.IsValid)
            {
                var customer = await context.Customers.FindAsync(model.CustomerID);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.City = model.City;
                customer.Country = model.Country;

                customer.Address = model.Address;
                customer.Phone = model.Phone;
                customer.Email = model.Email;

                context.Entry(customer).State = EntityState.Modified;
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = "The information has been successfully updated.";

                return RedirectToAction("ViewProfile", new { id = customer.CustomerID });
            }

            return View(model);
        }
        public async Task<ActionResult> ViewProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

    }
}
