using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pronia1.Models;

namespace Pronia1.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {

        }
       public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<Color> colors { get; set; }
        public DbSet<ProductColor> productColors { get; set; }
    }
}
