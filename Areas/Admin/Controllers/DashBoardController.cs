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
        // GET: Admin/DashBoard
        private ModelShoeStore context = new ModelShoeStore();
        public ActionResult Index()
        {
            var brand = (from m in context.Brands
                         orderby m.BrandID descending
                        select new BrandViewModel
                        {
                            BrandId = m.BrandID,
                            BrandName = m.BrandName,
                        }).ToList();
            
            return View(brand);
        }
        public ActionResult Category()
        {
            var category = (from m in context.Categories
                            select new CategoryViewModel
                            {
                                CategoryID = m.CategoryID,
                                CategoryName = m.CategoryName,
                            }).ToList();
            return PartialView(category);
        }
    }
}