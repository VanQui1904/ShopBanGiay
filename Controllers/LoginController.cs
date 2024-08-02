using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private ModelShoeStore context = new ModelShoeStore();
        [HttpGet]
        public ActionResult Login(string username, string pass)
        {
            //var user = context.Accounts.Include(u => u.Role)
            //                 .FirstOrDefault(u => u.Username == username && u.Password == pass);
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Account formdata)
        {
            var sig = new Account();
            sig.Username = formdata.Username;
            sig.Password = formdata.Password;
            sig.Role = "Customer";
            sig.Email = formdata.Email;
            sig.Phone = formdata.Phone;
            var comfirmPass = "";
            if(comfirmPass != sig.Password)
            {
                ViewBag.message = "Incorrect password confirmation!!";
                return RedirectToAction("Signup", "Login");
            }
            else
            {
                context.Accounts.Add(sig);
                context.SaveChanges();
            }
            return RedirectToAction("Login","Login");
        }
    }
}