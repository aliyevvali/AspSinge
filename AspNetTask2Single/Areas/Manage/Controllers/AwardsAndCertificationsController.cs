using AspNetTask2Single.DAL;
using AspNetTask2Single.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspNetTask2Single.Areas.Manage.Controllers
{ 
    [Area("Manage")]
    public class AwardsAndCertificationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envio;

        public AwardsAndCertificationsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _envio = environment;
        }
        public IActionResult Index()
        {
            List<AwardsAndCertifications> aw = _context.AwardsAndCertifications.ToList();
            return View(aw);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AwardsAndCertifications aw)
        {
            _context.AwardsAndCertifications.Add(aw);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            AwardsAndCertifications awards = _context.AwardsAndCertifications.FirstOrDefault(x => x.Id == id);
            if (awards == null) return NotFound();
            return View(awards);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AwardsAndCertifications aw)
        {
            AwardsAndCertifications awardsAnd = _context.AwardsAndCertifications.FirstOrDefault(x => x.Id == aw.Id);
            if (awardsAnd == null) return NotFound();
            awardsAnd.Awards = aw.Awards;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            AwardsAndCertifications awards = _context.AwardsAndCertifications.FirstOrDefault(x => x.Id == id);
            if (awards == null) return NotFound();
            _context.AwardsAndCertifications.Remove(awards);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
