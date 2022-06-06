using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pronia1.DAL;
using Pronia1.Models;
using Pronia1.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pronia1.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context {get;}
        public HomeController(AppDbContext context)
        {
                _context=context;
        }

        public IActionResult Index()
        {

            HomeVM home = new HomeVM
            {
                sliders =  _context.Sliders.ToList(),
                products =_context.products.Include(p=>p.productImages).Take(8).ToList(),
                categories=_context.categories.ToList()
            };
            return View(home);
        }

       

       
        
    }
}
