using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCoreProductOperationApplication.Data;
using MvcCoreProductOperationApplication.Models;
using MvcCoreProductOperationApplication.Models.CustomDropdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProductOperationApplication.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDBContext context;
        public List<SelectListItem> Categories { get; set; }

        private readonly int StockCount = 0;
        public ProductController(ApplicationDBContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Index(string searchString, int? searchMinValue, int? searchMaxValue)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentMinValue"] = searchMinValue;
            ViewData["CurrentMaxValue"] = searchMaxValue;

            try
            {
                var products = from product in context.Products.Where(x => x.StockQuantity > StockCount && x.IsDeleted == false).Include(x=>x.Category)
                               from category in context.Categories.Where(x => product.CategoryId == x.Id && product.StockQuantity >= x.MinStockQuantity)

                               select product;


                if (!String.IsNullOrEmpty(searchString))
                {
                    products = products.Where(s => s.Title.Contains(searchString)
                                           || s.Category.Name.Contains(searchString) || s.Description.Contains(searchString));
                }

                if (searchMinValue is not null && searchMaxValue is not null)
                {
                    products = products.Where(x => x.StockQuantity <= searchMaxValue && x.StockQuantity >= searchMinValue);
                }

                return View(await products.AsNoTracking().ToListAsync());
            }
            catch (Exception e)
            {
                e.Message.ToString();
                throw;
            }

        }

       private void GetAllCategories() 
        {
            Categories = context.Categories.Select(a =>
                              new SelectListItem
                              {
                                  Value = a.Id,
                                  Text = a.Name
                              }).ToList();

            ViewBag.listOfProductCategorty = Categories;
        }

        public IActionResult Create()
        {
            GetAllCategories();
            return View();
        }
                
        [HttpPost]
        public IActionResult Create(Product model)
        {
            GetAllCategories();

            try
            {
                if (!ModelState.IsValid)
                {
                    var message = ModelState.ToList();
                    return View(model);
                }

                Product newProduct = new Product
                {
                    Title = model.Title,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    StockQuantity = model.StockQuantity,
                };
                context.Products.Add(newProduct);
                context.SaveChanges();
                return Redirect("/Product");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes");
                throw;
            }

        }


        public IActionResult Edit(int id)
        {
            GetAllCategories();
            Product product = context.Products.Where(e => e.Id == id).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = ModelState.ToList();
                    return View(model);
                }

                Product editProduct = new Product
                {   Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    StockQuantity = model.StockQuantity,
                };
                context.Products.Update(editProduct);
                context.SaveChanges();
                return Redirect("/Product");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to edit changes");
                throw;
            }
        }


        public IActionResult Delete(int id)
        {
            Product product = context.Products.Where(e => e.Id == id).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product model)
        {
            try
            {
                Product product = context.Products.Where(e => e.Id == model.Id).FirstOrDefault();
                product.IsDeleted = true;
                context.Products.Update(product);
                context.SaveChanges();
                return Redirect("/Product");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to delete");
                throw;
            }
        }
    }
}
