using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSmartClient01.Models;
using System.Threading.Tasks;

namespace MVCSmartClient01.Controllers
{
    public class ATest1Controller : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = await this.GetFullAndPartialViewModel();
            return this.View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetCategoryProducts(string categoryId)
        {
            var lookupId = int.Parse(categoryId);
            var model = await this.GetFullAndPartialViewModel(lookupId);
            return PartialView("CategoryResults", model);
        }

        private FullAndPartialViewModel GetModelByCategory(int intId)
        {
            FullAndPartialViewModel modelReturn = new FullAndPartialViewModel();
            List<CategoryProductItem> Product1 = new List<CategoryProductItem>() { new CategoryProductItem(){ItemProduct = "Test11"} };
            List<CategoryListItem> Kategori1 = new List<CategoryListItem>() { 
                new CategoryListItem(){CategoryId = 1, CategoryName = "11Test"},
                new CategoryListItem(){CategoryId = 2, CategoryName = "22Test"}};
            List<CategoryProductItem> Product2 = new List<CategoryProductItem>() { new CategoryProductItem() { ItemProduct = "Test22" } };
            List<CategoryListItem> Kategori2 = new List<CategoryListItem>() { new CategoryListItem() { CategoryId = 2, CategoryName = "22Test" } };
            if (intId == 0)
            {
                modelReturn = new FullAndPartialViewModel() { CategoryId = 1, Products = Product1, CategoryList = Kategori1 };
            }
            if (intId == 1)
            {
                modelReturn = new FullAndPartialViewModel() { CategoryId = 1, Products = Product1, CategoryList = Kategori1 };
            }
            if (intId == 2)
            {
                modelReturn = new FullAndPartialViewModel() { CategoryId = 2, Products = Product2, CategoryList = Kategori2 };
            }
            
            return modelReturn;
        }

        private async Task<FullAndPartialViewModel> GetFullAndPartialViewModel(int categoryId = 0)
        {
            // populate the viewModel and return it
            var fullAndPartialViewModel = GetModelByCategory(categoryId);
            return fullAndPartialViewModel;
        }
    }
}