using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Models;
using Store.ViewModels;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.Controllers
{
    public class ProductsController : Controller
    {
        private StoreContext db;
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public ProductsController(StoreContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            db = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult CategoryProperties() => View( db.SubCategories.ToList());
        
        public IActionResult ProductsProperties()
        {
            List<SubCategory> subCategories = db.SubCategories.ToList();
            ProductListViewModel products = new ProductListViewModel
            {
                Products = null,
                SubCategories = new SelectList(subCategories, "Id", "SubCategoryName")
            };
            return View(products);
        }
        [HttpPost]
        public IActionResult ProductsProperties(int? category, string name)
        {
            List<SubCategory> subCategories = db.SubCategories.ToList();
            IEnumerable<Product> product = null; 
            if (category != null)
            {
                product = db.Products.Where(p => p.SubCategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                product = db.Products.Where(p => p.Name == name);
            }
            ProductListViewModel products = new ProductListViewModel
            {
                Products = product.ToList(),
                SubCategories = new SelectList(subCategories, "Id", "SubCategoryName")
            };
            return View(products);
        }
        public IActionResult CreateCategory() => View();
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryProperties");
        }
        public IActionResult CreateSubCategory() 
        {
            List<Category> categories = db.Categories.ToList();
            ViewBag.categories = new SelectList(categories, "Id", "CategoryName");
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategory category, Dictionary<string, List<TwoParam>> charactesristicProduct = null)
        {
            if (charactesristicProduct != null)
            {
                category.PatternСharacteristics = JsonSerializer.Serialize(charactesristicProduct);
            }
            db.SubCategories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryProperties");
        }
        public IActionResult CreateProduct()
        {
            List<SubCategory> subCategories = db.SubCategories.ToList();
            ViewBag.subCategories = new SelectList(subCategories, "Id", "SubCategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile uploadedFile = null, Dictionary<string, List<TwoParam>> charactesristicProduct = null)
        {
            if (uploadedFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
                product.Img = imageData;
            }
            if (charactesristicProduct != null)
            {
                product.Characteristic = JsonSerializer.Serialize(charactesristicProduct);
            }
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            product.UserId = user.Id;
            product.IsFavorite = true;
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryProperties");
        }
        public IActionResult EditProduct(int id = -1)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == id);
            Dictionary<string, List<TwoParam>> dict = null;
            List<SubCategory> subCategories = db.SubCategories.ToList();
            ViewBag.subCategories = new SelectList(subCategories, "Id", "SubCategoryName");
            if (product.Characteristic != null)
            {
                dict = JsonSerializer.Deserialize<Dictionary<string, List<TwoParam>>>(product.Characteristic);
            }
            return View((product, dict));
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile uploadedFile = null, Dictionary<string, List<TwoParam>> charactesristicProduct = null)
        {
            if (uploadedFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
                product.Img = imageData;
            }
            if (charactesristicProduct != null)
            {
                product.Characteristic = JsonSerializer.Serialize(charactesristicProduct);
            }
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            product.UserId = user.Id;
            product.IsFavorite = true;
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryProperties");
        }
        public ActionResult SelectSubCategoryPattern(int? category)
        {
            List<SubCategory> subCategories = db.SubCategories.ToList();
            SelectList subCategoryList = new SelectList(subCategories, "Id", "SubCategoryName");
            SubCategory subCategory = null;
            Dictionary<string, List<TwoParam>> dict = null;
            if (category != null) 
            { 
                subCategory = db.SubCategories.FirstOrDefault(s => s.Id == category);
                if (subCategory.PatternСharacteristics != null)
                {
                    dict = JsonSerializer.Deserialize<Dictionary<string, List<TwoParam>>>(subCategory.PatternСharacteristics);
                }
            }
            ViewBag.ErrorMessage = "This category not have template.";
            return PartialView("SelectSubCategoryPattern", dict);
        }
        public IActionResult CreateCharasteristicOrChange(int id = -1)
        {
            SubCategory category = db.SubCategories.FirstOrDefault(c => c.Id == id);
            Dictionary<string, List<TwoParam>> dict = null;
            if (category.PatternСharacteristics != null)
            {
                dict = JsonSerializer.Deserialize<Dictionary<string, List<TwoParam>>>(category.PatternСharacteristics);
            }
            List<Category> categories = db.Categories.ToList();
            ViewBag.categories = new SelectList(categories, "Id", "CategoryName");
            return View((category, dict));
        }
        [HttpPost]
        public async Task<IActionResult> CreateCharasteristicOrChange(SubCategory forId, Dictionary<string, List<TwoParam>> charactesristicModel = null) 
        {
            SubCategory category = db.SubCategories.FirstOrDefault(c => c.Id == forId.Id);
            if (category != null && charactesristicModel != null)
            {
                category.PatternСharacteristics = JsonSerializer.Serialize(charactesristicModel);
                db.SubCategories.Update(category);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("CategoryProperties");
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            SubCategory category = db.SubCategories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                db.SubCategories.Remove(category);
                await db.SaveChangesAsync();
            }
            //return View();
            return RedirectToAction("CategoryProperties");
        }
    }
}
