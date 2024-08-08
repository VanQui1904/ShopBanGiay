using ShoeStoreProject.Areas.Admin.Data;
using ShoeStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeStoreProject.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private ModelShoeStore context = new ModelShoeStore();

        public ActionResult Index(string searchTerm)
        {
            var viewModel = new BrandCategoryViewModel
            {
                // Tìm kiếm thương hiệu
                Brands = context.Brands
                    .Where(b => string.IsNullOrEmpty(searchTerm) || b.BrandName.Contains(searchTerm))
                    .Select(b => new BrandViewModel
                    {
                        BrandId = b.BrandID,
                        BrandName = b.BrandName
                    }).ToList(),

                // Tìm kiếm danh mục
                Categories = context.Categories
                    .Where(c => string.IsNullOrEmpty(searchTerm) || c.CategoryName.Contains(searchTerm))
                    .Select(c => new CategoryViewModel
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName
                    }).ToList(),

                SearchTerm = searchTerm
            };

            return View(viewModel);
        }


        [HttpGet]
        public ActionResult AddBrand()
        {
            var br = new BrandViewModel();
            return View(br);
        }

        [HttpPost]
        public ActionResult AddBrand(BrandViewModel formdata)
        {
            if (ModelState.IsValid)
            {
                var addbr = new Brand
                {
                    BrandName = formdata.BrandName
                };
                context.Brands.Add(addbr);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Thương hiệu đã được thêm thành công.";
                return RedirectToAction("Index", "Brand");
            }

            return View(formdata);
        }

        [HttpGet]
        public ActionResult EditBrand(int id)
        {
            var editbr = (from m in context.Brands.Where(m => m.BrandID == id)
                          select new BrandViewModel
                          {
                              BrandId = m.BrandID,
                              BrandName = m.BrandName,
                          }).FirstOrDefault();

            if (editbr == null)
            {
                return RedirectToAction("Index", "Brand");
            }
            return View(editbr);
        }

        [HttpPost]
        public ActionResult EditBrand(BrandViewModel formdata)
        {
            if (ModelState.IsValid)
            {
                var edit = context.Brands.FirstOrDefault(x => x.BrandID == formdata.BrandId);
                if (edit != null)
                {
                    edit.BrandName = formdata.BrandName;
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Thương hiệu đã được cập nhật thành công.";
                    return RedirectToAction("Index", "Brand");
                }
            }

            return View(formdata);
        }

        [HttpGet]
        public ActionResult DeleteBrand(int id)
        {
            var brand = context.Brands
                .Where(b => b.BrandID == id)
                .Select(b => new BrandViewModel
                {
                    BrandId = b.BrandID,
                    BrandName = b.BrandName
                })
                .FirstOrDefault();

            if (brand == null)
            {
                return RedirectToAction("Index", "Brand");
            }

            return View("DeleteBrand", brand);
        }

        [HttpPost]
        public ActionResult DeleteBrand(BrandViewModel model)
        {
            var brand = context.Brands.Find(model.BrandId);
            if (brand == null)
            {
                return RedirectToAction("Index", "Brand");
            }

            try
            {
                context.Brands.Remove(brand);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Thương hiệu đã được xóa thành công.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Không thể xóa thương hiệu do khóa ngoại.";
            }

            return RedirectToAction("Index", "Brand");
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel formdata)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category
                {
                    CategoryName = formdata.CategoryName
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Danh mục đã được thêm thành công.";
                return RedirectToAction("Index", "Brand");
            }

            return View(formdata);
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var editbr = (from m in context.Categories.Where(m => m.CategoryID == id)
                          select new CategoryViewModel
                          {
                              CategoryID = m.CategoryID,
                              CategoryName = m.CategoryName,
                          }).FirstOrDefault();

            if (editbr == null)
            {
                return RedirectToAction("Index", "Brand");
            }
            return View(editbr);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel formdata)
        {
            if (ModelState.IsValid)
            {
                var edit = context.Categories.FirstOrDefault(x => x.CategoryID == formdata.CategoryID);
                if (edit != null)
                {
                    edit.CategoryName = formdata.CategoryName;
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Danh mục đã được cập nhật thành công.";
                    return RedirectToAction("Index", "Brand");
                }
            }

            return View(formdata);
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var category = context.Categories
                .Where(c => c.CategoryID == id)
                .Select(c => new CategoryViewModel
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName
                })
                .FirstOrDefault();

            if (category == null)
            {
                return RedirectToAction("Index", "Brand");
            }

            return View("DeleteCategory", category);
        }

        [HttpPost]
        public ActionResult DeleteCategory(CategoryViewModel model)
        {
            var category = context.Categories.Find(model.CategoryID);
            if (category == null)
            {
                return RedirectToAction("Index", "Brand");
            }

            try
            {
                context.Categories.Remove(category);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Danh mục đã được xóa thành công.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Không thể xóa danh mục do khóa ngoại.";
            }

            return RedirectToAction("Index", "Brand");
        }
    }
}
