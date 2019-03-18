using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSmartClient01.Models
{
    public class CategoryProductItem
    {
        public string ItemProduct { get; set; }
    }
    public class CategoryListItem
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class FullAndPartialViewModel
    {
        public int CategoryId { get; set; }
        public List<CategoryProductItem> Products { get; set; }
        public List<CategoryListItem> CategoryList { get; set; }
    }
}