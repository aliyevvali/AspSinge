using AspNetTask2Single.DAL;
using AspNetTask2Single.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTask2Single.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EducationController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envio;

        public EducationController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _envio = environment;
        }
        public IActionResult Index()
        {
            List<Education> education = _context.Educations.ToList();
            return View(education);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Education education)
        {
            _context.Educations.Add(education);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Education education = _context.Educations.FirstOrDefault(x => x.Id == id);
            if (education == null) return NotFound();
            return View(education);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Education education)
        {
            Education education1 = _context.Educations.FirstOrDefault(x => x.Id == education.Id);
            if (education1 == null) return NotFound();
            education1.Faculty = education.Faculty;
            education1.Profession = education.Profession;
            education1.Uni = education.Uni;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Education education = _context.Educations.FirstOrDefault(x => x.Id == id);
            if (education == null) return NotFound();
            _context.Educations.Remove(education);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
