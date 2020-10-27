using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Models;

namespace Store.Components
{
    public class Catalog : ViewComponent
    {
        private readonly StoreContext db;
        public Catalog(StoreContext context)
        {
            db = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Category> categories = db.Categories.ToList();
            List<SubCategory> subCategories = db.SubCategories.ToList();
            return View("Catalog", (categories, subCategories));
        }
    }
}
