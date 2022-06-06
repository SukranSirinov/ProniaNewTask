using System.Collections.Generic;

namespace Pronia1.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductColor> productColors { get; set; }
    }
}
