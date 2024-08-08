using ShoeStoreProject.Areas.Admin.Data;
using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        private ModelShoeStore context = new ModelShoeStore();

        [HttpGet]
        //[Authorize(Role = "Admin")]
        public ActionResult RevenueByDate()
        {
            //if (Session["Role"] == null || Session["Role"].ToString() != "Customer")
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            var salesData = context.Orders
                .Where(o => o.OrderDate.HasValue) // Đảm bảo OrderDate có giá trị
                .ToList() // Chuyển dữ liệu từ cơ sở dữ liệu sang danh sách
                .GroupBy(o => o.OrderDate.Value.Date) // Nhóm dữ liệu theo ngày
                .Select(g => new RevenueByDateViewModel
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(o => o.TotalAmount)
                })
                .ToList();
            ViewBag.hoadon = context.Orders.Count();
            var totalAmount = context.Orders.Sum(invoice => invoice.TotalAmount);

            // Gán tổng số tiền cho ViewBag
            ViewBag.TotalAmount = totalAmount;
            return View(salesData);
        }
    }
}
