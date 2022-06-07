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
    public class ModelsListController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _envio;

        public ModelsListController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _envio = environment;
        }
        public IActionResult Index()
        {
            List<List> lists = _context.Lists.ToList();     
            return View(lists);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(List list)
        {
            if (_context.Lists.FirstOrDefault(x => x.Name.ToLower().Trim() == list.Name.ToLower().Trim()) != null) return RedirectToAction("Index");
            if (list.Photo.CheckSize(500) || !list.Photo.CheckType("image/"))
            {
                return RedirectToAction(nameof(Index));
            }
            list.Image = await list.Photo.SavaFileAsync(Path.Combine(_envio.WebRootPath, "imgs","pp"));
            await _context.Lists.AddAsync(list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //public IActionResult Update(int Id, List list)
        //{
        //    List list1 = _context.Lists.Find(Id);

        //    if (list1 == null || list == null) return NotFound();
        //    list1.Name = list.Name;
        //    list1.Image= list.Image;
        //    _context.Lists.Update(list1);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
        public IActionResult Delete(int id)
        {
            List list = _context.Lists.FirstOrDefault(x=>x.Id==id);
            if (list == null) return NotFound();
            _context.Lists.Remove(list);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
