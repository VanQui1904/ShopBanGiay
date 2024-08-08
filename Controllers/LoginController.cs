using Org.BouncyCastle.Crypto.Generators;
using ShoeStoreProject.Models;
using ShoeStoreProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ShoeStoreProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private ModelShoeStore context = new ModelShoeStore();
        [HttpGet]
        public ActionResult Login()
        {
            //var user = context.Accounts.Include(u => u.Role)
            //                 .FirstOrDefault(u => u.Username == username && u.Password == pass);
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            //var sig = new Account();
            //sig.Username = formdata.Username;
            //sig.Password = formdata.Password;
            //sig.Role = "Customer";
            //sig.Email = formdata.Email;
            //sig.Phone = formdata.Phone;
            //var comfirmPass = "";
            //if(comfirmPass != sig.Password)
            //{
            //    ViewBag.message = "Incorrect password confirmation!!";
            //    return RedirectToAction("Signup", "Login");
            //}
            //else
            //{
            //    context.Accounts.Add(sig);
            //    context.SaveChanges();
            //}
            //return RedirectToAction("Login","Login");
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem username đã tồn tại chưa
                if (context.Accounts.Any(a => a.Username == model.Username))
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View(model);
                }

                // Tạo một bản ghi Customer trống
                var customer = new Customer();
                context.Customers.Add(customer);
                await context.SaveChangesAsync();

                // Tạo tài khoản với thông tin từ model
                var account = new Account
                {
                    Username = model.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // mã hóa mật khẩu
                    Role = "Customer",
                    Phone = model.Phone,
                    Email = model.Email,
                    CustomerID = customer.CustomerID,
                };

                context.Accounts.Add(account);
                await context.SaveChangesAsync();

                // Redirect to Login after successful registration
                return RedirectToAction("Login", "Login");
            }
            return View(model);
        }


        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = await context.Accounts
                    .FirstOrDefaultAsync(a => a.Username == model.Username);

                if (account != null && BCrypt.Net.BCrypt.Verify(model.Password, account.Password))
                {
                    // Tạo session hoặc cookie để lưu trạng thái đăng nhập
                    Session["UserID"] = account.AccountID;
                    Session["Username"] = account.Username;
                    Session["CustomerID"] = account.CustomerID;
                    Session["Role"] = account.Role;
                    Session["Login"] = true;

                    if (account.Role == "Admin")
                    {
                        // Điều hướng tới Dashboard/RevenueByDate trong area Admin
                        return RedirectToAction("RevenueByDate", "DashBoard", new { area = "Admin" });
                    }
                    else if (account.Role == "Customer")
                    {
                        return RedirectToAction("Index", "Home"); // Điều hướng tới trang người dùng
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }


        public ActionResult Logout ()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    
}
}