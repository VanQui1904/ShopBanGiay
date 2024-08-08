using PagedList;
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
       // [Authorize(Roles = "Admin")]
        public ActionResult Products(int? page, string searchTerm)
        {
            //if (Session["Role"] == null || Session["Role"].ToString() != "Customer")
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            var item = (from m in context.Products.OrderByDescending(m => m.ProductID)
                        join br in context.Brands on m.BrandID equals br.BrandID into brtemp
                        join cate in context.Categories on m.CategoryID equals cate.CategoryID into catetemp
                        from brf in brtemp.DefaultIfEmpty()
                        from catf in catetemp.DefaultIfEmpty()
                        select new ProductsDisplay
                        {
                            ProductID = m.ProductID,
                            proName = m.proName,
                            Price = m.Price,
                            BrandName = brf != null ? brf.BrandName : "",
                            proImage = m.proImage,
                            Size = m.Size,
                            Color = m.Color,
                            Stock = m.Stock,
                            CategoryName = catf != null ? catf.CategoryName : "",
                        });

            // Kiểm tra nếu có từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                item = item.Where(x => x.proName.Contains(searchTerm) || x.BrandName.Contains(searchTerm));
                ViewBag.SearchTerm = searchTerm;
            }

            var itemList = item.ToList();

            if (itemList.Count == 0)
            {
                ViewBag.NoResultsMessage = "Không tìm thấy sản phẩm nào phù hợp với từ khóa.";
            }

            if (page == null)
            {
                page = 1;
            }

            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            ViewBag.message = itemList.Count();
            return View(itemList.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult AddProducts()
        {
            var pro = new ProductsViewModel();
            var lstBrand = context.Brands.OrderBy(x => x.BrandName).ToList();
            var lstcate = context.Categories.OrderBy(x => x.CategoryName).ToList();
            ViewBag.BrandID = new SelectList(lstBrand, "BrandID","BrandName");
            ViewBag.CategoryID = new SelectList(lstcate, "CategoryID", "CategoryName");
            return View(pro);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult AddProducts(ProductsViewModel formdata, HttpPostedFileBase fileUpload)
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
            TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
            return RedirectToAction("Products", "QLProducts");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult EditProducts(int id)
        {
            var pro = (from m in context.Products
                       join br in context.Brands on m.BrandID equals br.BrandID into brtemp
                       join cate in context.Categories on m.CategoryID equals cate.CategoryID into catetemp
                       from brf in brtemp.DefaultIfEmpty()
                       from catf in catetemp.DefaultIfEmpty()
                       orderby m.ProductID descending
                       where m.ProductID == id
                       select new ProductsViewModel
                       {
                           ProductID = m.ProductID,
                           proName = m.proName,
                           Price = m.Price,
                           proImage = m.proImage,
                           BrandID = m.BrandID,
                           Size = m.Size,
                           Color = m.Color,
                           Stock = m.Stock,
                           CategoryID = m.CategoryID,

                       }).FirstOrDefault();
            var lstBrand = context.Brands.OrderBy(x => x.BrandName).ToList();
            var lstcate = context.Categories.OrderBy(x => x.CategoryName).ToList();
            ViewBag.BrandID = new SelectList(lstBrand, "BrandID", "BrandName");
            ViewBag.CategoryID = new SelectList(lstcate, "CategoryID", "CategoryName");

            if (pro == null)
            {
                return RedirectToAction("Products", "QLProducts");
            }
            return View(pro);

        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult EditProducts(ProductsViewModel formdata,HttpPostedFileBase fileUpload)
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
            editpro.proImage = filename;
            context.SaveChanges();
            TempData["SuccessMessage"] = "Chỉnh sửa sản phẩm thành công!";

            return RedirectToAction("Products", "QLProducts");

        }

       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var product = context.Products
                .Where(x => x.ProductID == id)
                .FirstOrDefault();

            if (product == null)
            {
                TempData["ErrorMessage"] = "Sản phẩm không tồn tại!";
                return RedirectToAction("Products");
            }

            var viewModel = new ProductsViewModel
            {
                ProductID = product.ProductID,
                proName = product.proName,
                Price = product.Price,
                Size = product.Size,
                Color = product.Color,
                Stock = product.Stock,
                BrandID = product.BrandID,
                CategoryID = product.CategoryID,
                proImage = product.proImage
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("DeleteProduct")]
       // [Authorize(Roles = "Admin")]
        public ActionResult DeleteProductConfirmed(int id)
        {
            var itemdel = context.Products.Where(x => x.ProductID == id).FirstOrDefault();

            if (itemdel == null)
            {
                TempData["ErrorMessage"] = "Sản phẩm không tồn tại!";
                return RedirectToAction("Products");
            }
            
            context.Products.Remove(itemdel);
            context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Products");
        }

    }
}