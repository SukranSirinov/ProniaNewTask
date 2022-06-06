using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pronia1.DAL;
using Pronia1.Models;
using Pronia1.Utilies.File;
using Pronia1.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pronia1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get;  }

        private IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context=context;
            _env = env;
        }
        public async Task<ActionResult> Index()
        {
           List<Product> products = await _context.products.Include(p=>p.productImages).Include(p=>p.Category).ToListAsync();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
           
            ViewBag.Categories= _context.categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(Product product)
        {
            ViewBag.Categories = _context.categories.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.products.Any(p=>p.Name==product.Name))
            {
                ModelState.AddModelError("Name", "This name already exisst");
                return View();
            }
            foreach (var item in product.Photos)
            {
                if (item.CheckSize(200))
                {
                    ModelState.AddModelError("Photos", $"{item.FileName}Bu sekil 200 kb coxdur");
                    return View();  
                }
                if (!item.CheckType("image/"))
                {
                    ModelState.AddModelError("Photos", $"{item.FileName}Bu sekil deyil");
                    return View();
                }
            }
            product.productImages = new List<ProductImage>();
            foreach (var photo in product.Photos)
            {
                string fileName = await photo.SaveFileAsync(Path.Combine(_env.WebRootPath,"assets","images","product"));
                ProductImage image = new ProductImage
                {
                   Url = await  photo.SaveFileAsync(Path.Combine(_env.WebRootPath, "assets", "images", "product")),
                   IsMain=false,
                   Product=product,
                   
                };
                product.productImages.Add(image);
            }
            if (product.MainPhoto.CheckSize(200))
            {
                ModelState.AddModelError("Photos", "Bu sekil 200 kb coxdur");
                return View();
            }
            if (!product.MainPhoto.CheckType("image/"))
            {
                ModelState.AddModelError("Photos","Bu sekil deyil");
                return View();
            }
            product.productImages.Add(new ProductImage
            {
                Url = await product.MainPhoto.SaveFileAsync(Path.Combine(_env.WebRootPath, "assets", "images", "product")),
                IsMain=true,
                Product = product,

            }) ;

            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
           Product product= _context.products.Include(p=>p.Category).Include(p=>p.productImages).SingleOrDefault(p=>p.Id==id);
            ViewBag.Categories = _context.categories.ToList();
            
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            return Json(product.ImgId);
            
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
