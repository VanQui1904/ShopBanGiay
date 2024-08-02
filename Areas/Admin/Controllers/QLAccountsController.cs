using ShoeStoreProject.Areas.Admin.Data;
using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Areas.Admin.Controllers
{
    public class QLAccountsController : Controller
    {
        // GET: Admin/QLAccounts
        private ModelShoeStore context = new ModelShoeStore();
        public ActionResult Accounts()
        {
            var itemAcc = from m in context.Accounts
                          select new AccountViewModel()
                          {
                              AccountID = m.AccountID,
                              Username = m.Username,
                              
                              Role = m.Role,
                              Email = m.Email,
                              Phone = m.Phone
                          };
            return View(itemAcc);
        }
        [HttpGet]
        public ActionResult RoleAccount(int id)
        {
            var acc = new AccountViewModel();
            var roleAcc = context.Accounts.OrderBy(x => x.Role).ToList();
            ViewBag.Role = new SelectList(roleAcc, "AccountID", "Role");
            return View(acc);
        }
        [HttpPost]
        public ActionResult RoleAccount(AccountViewModel formdata)
        {
            var role = context.Accounts.Where(x=>x.AccountID == formdata.AccountID).FirstOrDefault();
            if (role == null)
            {
                return RedirectToAction("Accounts", "QLAccounts");
            }
            role.Role = "Admin";

            context.SaveChanges();
            return View(role);
            
        }
    }
}