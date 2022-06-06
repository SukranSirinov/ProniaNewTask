using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia1.Models
{
    public class Product
    {
        public int Id { get; set; }     
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Raiting { get; set; }
        [Required]
        public int StockCount { get; set; }
        [Required]
        public int CategoryId { get; set; }
       public bool IsDelete { get; set; }
        public Category Category { get; set; }
        public List<ProductImage> productImages { get; set; }
        [NotMapped]
        public List<ProductColor> productColors { get; set; }
        [NotMapped]
        public IFormFileCollection Photos { get; set; }
        [NotMapped]
        public IFormFile MainPhoto { get;set; }
        [NotMapped]
        public List<int> ImgId { get; set; }


    }
}
