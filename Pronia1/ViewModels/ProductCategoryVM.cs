using Pronia1.Models;
using System.Collections.Generic;

namespace Pronia1.ViewModels
{
    public class ProductCategoryVM
    {
        public Product Product {get;set;}
        public List<Category> Categories { get;set;}
    }
}
