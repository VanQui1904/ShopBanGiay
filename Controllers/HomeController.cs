using ShoeStoreProject.Models;
using ShoeStoreProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;


namespace ShoeStoreProject.Controllers

{
    public class HomeController : Controller
    {
        private ModelShoeStore _context = new ModelShoeStore();
        // GET: QLGiay
        public ActionResult Index(int? page, string searchName, decimal? minPrice, decimal? maxPrice)
        {
            
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            // Tìm kiếm sản phẩm dựa trên tên và giá
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                products = products.Where(p => p.proName.Contains(searchName));

            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            // Sắp xếp theo giá giảm dần
            products = products.OrderByDescending(p => p.Price);

            // Phân trang
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);

            // Đếm số lượng sản phẩm
            int soluong = pagedProducts.Count();
            ViewBag.soluong = soluong;

            return View(pagedProducts);



        }
        //public ActionResult TimKiem(int maloaisp= 0 , string SearchString="")
        //{
        //    if(SearchString !="")
        //    {
        //        var sanPhams = _context.Products(s  => s.)
        //    }
        //}
        [HttpGet]
        
        public ActionResult DetailGiay(int productID)
        {
            var giay = (from p in _context.Products
                        where p.ProductID == productID
                        select new ProductsVM
                        {
                            proName = p.proName,
                            Price = p.Price,
                            proDescription = p.proDescription,
                            Stock = p.Stock,
                            Size = p.Size,
                            proImage = p.proImage,
                            BrandID = p.BrandID
                        }).FirstOrDefault();

            if (giay == null)
            {
                ViewBag.message = "Sản phẩm đã hết hàng";
                return RedirectToAction("Index", "Home");
            }

            return View(giay); // Truyền một đối tượng duy nhất
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}