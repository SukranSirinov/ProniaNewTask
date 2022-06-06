using Pronia1.Models;
using System.Collections.Generic;

namespace Pronia1.ViewModels
{
    public class HomeVM
    {
        public List<Category> categories { get; set; }  
        public List<Product> products { get; set; }
        public List<ProductImage>productImages  { get; set; }
        public List<Slider> sliders { get;set; }


    }
}
