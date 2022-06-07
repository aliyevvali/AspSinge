using AspNetTask2Single.DAL;
using AspNetTask2Single.Models;
using AspNetTask2Single.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTask2Single.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envio;

        public AboutController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _envio = environment;
        }
        public IActionResult Index()
        {
            List<AboutMe> aboutMe = _context.AboutMes.ToList();  
            return View(aboutMe);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutMe aboutMe)
        {
            if (aboutMe.Photo.CheckSize(500) || !aboutMe.Photo.CheckType("image/"))
            {
                return RedirectToAction(nameof(Index));
            }
            aboutMe.Image = await aboutMe.Photo.SavaFileAsync(Path.Combine(_envio.WebRootPath, "imgs", "aboutme"));
            await _context.AboutMes.AddAsync(aboutMe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            AboutMe aboutMe = _context.AboutMes.FirstOrDefault(x=>x.Id==id);
            if (aboutMe == null) return NotFound();
            return View(aboutMe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AboutMe aboutMe)
        {
            AboutMe aboutMe1 = _context.AboutMes.FirstOrDefault(x=>x.Id == aboutMe.Id);
            if (aboutMe1 == null) return NotFound();
            aboutMe1.Name = aboutMe.Name;
            aboutMe1.Title = aboutMe.Title;
            aboutMe1.Email = aboutMe.Email;
            aboutMe1.Contact = aboutMe.Contact;
            aboutMe1.Adress = aboutMe.Adress;
            if (aboutMe.Photo != null)
            {
                if (aboutMe.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size cant be higher than 500kb");
                    return RedirectToAction(nameof(Edit));
                }
                if (!aboutMe.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "File must be image");
                    return RedirectToAction(nameof(Edit));
                }
                aboutMe.Image = await aboutMe.Photo.SavaFileAsync(Path.Combine(_envio.WebRootPath, "imgs", "aboutme"));
                aboutMe1.Image = aboutMe.Image;
            }
            if (aboutMe.Image != null) aboutMe1.Image = aboutMe.Image;
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            AboutMe aboutMe = _context.AboutMes.FirstOrDefault(x => x.Id == id);
            if (aboutMe == null) return NotFound();
            _context.AboutMes.Remove(aboutMe);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
