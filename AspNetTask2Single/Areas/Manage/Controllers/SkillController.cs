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
    public class SkillController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envio;

        public SkillController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _envio = environment;
        }
        public IActionResult Index()
        {
            List<Skills> skills = _context.Skills.ToList();
            return View(skills);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skills skills)
        {
            if (skills.Photo.CheckSize(500) || !skills.Photo.CheckType("image/"))
            {
                return RedirectToAction(nameof(Index));
            }
            skills.Image = await skills.Photo.SavaFileAsync(Path.Combine(_envio.WebRootPath, "imgs", "skill"));
            await _context.Skills.AddAsync(skills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Skills skills = _context.Skills.FirstOrDefault(x => x.Id == id);
            if (skills == null) return NotFound();
            return View(skills);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Skills skills)
        {
            Skills skills1 = _context.Skills.FirstOrDefault(x => x.Id == skills.Id);
            if (skills1 == null) return NotFound();
            skills1.WorkFlow = skills.WorkFlow;
            if (skills.Photo != null)
            {
                if (skills.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size cant be higher than 500kb");
                    return RedirectToAction(nameof(Edit));
                }
                if (!skills.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "File must be image");
                    return RedirectToAction(nameof(Edit));
                }
                skills.Image = await skills.Photo.SavaFileAsync(Path.Combine(_envio.WebRootPath, "imgs", "aboutme"));
                skills1.Image = skills.Image;
            }
            if (skills.Image != null) skills1.Image = skills.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Skills skills = _context.Skills.FirstOrDefault(x => x.Id == id);
            if (skills == null) return NotFound();
            _context.Skills.Remove(skills);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
