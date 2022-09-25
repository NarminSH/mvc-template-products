using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_TemplateApp.Data;
using MVC_TemplateApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_TemplateApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductPhotosController : Controller
    {
        private readonly MyContext _context;

        public ProductPhotosController(MyContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var myContext = _context.ProductPhotos.Include(p => p.Product);
            return View(await myContext.ToListAsync());
        }

        public IActionResult Create(Guid? id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductPhoto productPhoto)
        {
            productPhoto.Product = _context.Products.FirstOrDefault(x => x.Id == productPhoto.ProductId);
            productPhoto.Url = "";
            productPhoto.Id = Guid.NewGuid();
            _context.Add(productPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(productPhoto);
        }
    }
}

