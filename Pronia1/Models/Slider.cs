using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia1.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bos qoymaq olmaz"),MaxLength(50)]
        public string Name  { get; set; }
        public int DiscountPercent { get; set; }
        public string Title { get; set; }
        
        public string  Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
