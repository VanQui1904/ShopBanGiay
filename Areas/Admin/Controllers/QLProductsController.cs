using ShoeStoreProject.Areas.Admin.Data;
using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ShoeStoreProject.Areas.Admin.Controllers
{
    public class QLProductsController : Controller
    {
        // GET: Admin/QLProducts
        private ModelShoeStore context = new ModelShoeStore();
        public ActionResult Products()
        {
            var item = from m in context.Products
                       select new ProductsDisplay
                       {
                           ProductID = m.ProductID,
                           ProductsName = m.proName,
                           Price = m.Price,
                           BrandID = m.BrandID,
                           Size = m.Size,
                           Color = m.Color,
                           Stock = m.Stock,
                           CategoryID = m.CategoryID,
                       };
            ViewBag.message = item.Count();
            return View(item);
        }
        [HttpGet]
        public ActionResult AddProducts()
        {
            var pro = new ProductsViewModel();
            var lstBrand = context.Brands.OrderBy(x => x.BrandName).ToList();
            var lstcate = context.Categories.OrderBy(x => x.CategoryName).ToList();
            ViewBag.BrandName = new SelectList(lstBrand, "BrandID","BrandName");
            ViewBag.CategoryName = new SelectList(lstcate, "CategoryID", "CategoryName");
            return View(pro);
        }
        [HttpPost]
        public ActionResult AddProducts(ProductsViewModel formdata, HttpPostedFile fileUpload)
        {
            var item = new Product();
            item.Price = formdata.Price;
            item.proName = formdata.proName;
            item.BrandID = formdata.BrandID;
            item.Size = formdata.Size;
            item.Color = formdata.Color;
            item.Stock = formdata.Stock;
            item.CategoryID = formdata.CategoryID;
            item.proDescription = formdata.proDescription;
            var filename = System.IO.Path.GetFileName(fileUpload.FileName);
            //getpath
            var path = Path.Combine(Server.MapPath("~/Image/"), filename);
            //ktr image
            if (System.IO.File.Exists(path))
            {
                ViewBag.message = "Ảnh này đã tồn tại!!";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            item.proImage = filename;
            context.Products.Add(item);
            context.SaveChanges();
            return RedirectToAction("Products", "QLproducts");
        }

        [HttpGet]
        public ActionResult EditProducts(int id)
        {
            var pro = (from m in context.Products
                       where m.ProductID == id
                       select new ProductsViewModel()
                       {
                           ProductID = m.ProductID,
                           proName = m.proName,
                           Price = m.Price,
                           BrandID = m.BrandID,
                           Size = m.Size,
                           Color = m.Color,
                           Stock = m.Stock,
                           CategoryID = m.CategoryID,
                           proDescription = m.proDescription,

                       }).FirstOrDefault();
            var lstBrand = context.Brands.OrderBy(x => x.BrandName).ToList();
            var lstcate = context.Categories.OrderBy(x => x.CategoryName).ToList();
            ViewBag.BrandName = new SelectList(lstBrand, "BrandID", "BrandName");
            ViewBag.CategoryName = new SelectList(lstcate, "CategoryID", "CategoryName");
            if (pro == null)
            {
                return RedirectToAction("Products", "QLProduct");
            }
            return View(pro);
        }
        [HttpPost]
        public ActionResult EditProducts(ProductsViewModel formdata)
        {
            var editpro = context.Products.Where(x => x.ProductID == formdata.ProductID).FirstOrDefault();
            if(editpro == null)
            {
                return RedirectToAction("Products", "QLProducts");
            }
            editpro.proDescription = formdata.proDescription;
            editpro.Price = formdata.Price;
            editpro.proName = formdata.proName;
            editpro.CategoryID = formdata.CategoryID;
            editpro.Stock = formdata.Stock;
            editpro.Size = formdata.Size;
            editpro.Color = formdata.Color;
            editpro.BrandID = formdata.BrandID;
            context.SaveChanges();
            return View(editpro);
        }
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var itemdel = context.Products.Where(x=>x.ProductID == id).FirstOrDefault();
            context.Products.Remove(itemdel);
            context.SaveChanges();
            return RedirectToAction("Products", "QLProducts");
        }
    }
}