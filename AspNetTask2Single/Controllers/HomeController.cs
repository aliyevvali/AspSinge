using AspNetTask2Single.DAL;
using AspNetTask2Single.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTask2Single.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context{ get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                awards = await _context.AwardsAndCertifications.ToListAsync(),
                skills = await _context.Skills.ToListAsync(),
                lists = await _context.Lists.ToListAsync(),
                interests =await _context.Interests.ToListAsync(),
                experiences = await _context.Experiences.ToListAsync(),
                educations = await _context.Educations.ToListAsync(),
                aboutMes = await _context.AboutMes.ToListAsync()
            };
            return View(homeVM);
        }
    }
}
