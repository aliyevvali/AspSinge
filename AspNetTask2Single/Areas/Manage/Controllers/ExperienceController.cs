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
    public class ExperienceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envio;

        public ExperienceController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _envio = environment;
        }
        public IActionResult Index()
        {
            List<Experiences> experiences = _context.Experiences.ToList();
            return View(experiences);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Experiences experiences)
        {
            _context.Experiences.Add(experiences);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Experiences experiences = _context.Experiences.FirstOrDefault(x=>x.Id==id);
            if (experiences == null) return NotFound();
            return View(experiences);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Experiences experiences)
        {
            Experiences experiences1 = _context.Experiences.FirstOrDefault(x => x.Id == experiences.Id);
            if (experiences1 == null) return NotFound();
            experiences1.Position = experiences.Position;
            experiences1.AboutWork = experiences.AboutWork;
            experiences1.WorkPlace = experiences.WorkPlace;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Experiences experiences = _context.Experiences.FirstOrDefault(x => x.Id == id);
            if (experiences == null) return NotFound();
            _context.Experiences.Remove(experiences);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
