using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pronia1.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public List<Product> Product { get; set; }

    }
}
