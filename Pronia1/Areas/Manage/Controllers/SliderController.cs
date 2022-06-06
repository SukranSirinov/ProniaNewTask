using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia1.DAL;
using Pronia1.Models;
using Pronia1.Utilies.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pronia1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }

        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            bool IsExist = _context.Sliders.Any(s => s.Name.ToLower().Trim() == slider.Name.ToLower().Trim());
            if (IsExist) return View();
            if (slider.Photo.CheckSize(200))
            {
                ModelState.AddModelError("Photo", "File 200 kb dan boyukdur");
                return View();
            }
            if (!slider.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "File sekil deyil");
                return View();
            }
            string filaName = await slider.Photo.SaveFileAsync(Path.Combine(_env.WebRootPath, "assets", "images", "slider", "inner-img"));
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.Find(id);
            if (slider == null) return NotFound();
            FileExtension.DeleteFile(Path.Combine(_env.WebRootPath, "assets", "images", "slider", "inner-img", slider.Image));
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Update(int id)
        {
            Slider slider = _context.Sliders.Find(id);
            if (slider == null) return NotFound();

            return View(slider);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Slider slider)
        {
            if (slider.Id != id) return BadRequest();
            Slider sliderItem = _context.Sliders.Find(id);
            if (sliderItem != null) return NotFound();

            sliderItem.Name = slider.Name;
            sliderItem.Title = slider.Title;
            sliderItem.DiscountPercent = slider.DiscountPercent;
            sliderItem.Image = slider.Image;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));



        }
    }
}
