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
    public class InterestsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envio;

        public InterestsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _envio = environment;
        }
        public IActionResult Index()
        {
            List<Interests> interests = _context.Interests.ToList();
            return View(interests);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Interests interests)
        {
            _context.Interests.Add(interests);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Interests interests = _context.Interests.FirstOrDefault(x => x.Id == id);
            if (interests == null) return NotFound();
            return View(interests);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Interests interests)
        {
            Interests interests1 = _context.Interests.FirstOrDefault(x => x.Id == interests.Id);
            if (interests1 == null) return NotFound();
            interests1.Interest = interests.Interest;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Interests interests = _context.Interests.FirstOrDefault(x => x.Id == id);
            if (interests == null) return NotFound();
            _context.Interests.Remove(interests);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
