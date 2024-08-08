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

        //[Authorize(Roles = "Admin")]
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
            return View(itemAcc.ToList());
        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public JsonResult UpdateRole(int AccountID, string Role)
        {
            var account = context.Accounts.FirstOrDefault(a => a.AccountID == AccountID);
            if (account == null)
            {
                return Json(new { success = false, message = "Account not found" });
            }

            account.Role = Role;
            context.SaveChanges();

            return Json(new { success = true });
        }


    }
}