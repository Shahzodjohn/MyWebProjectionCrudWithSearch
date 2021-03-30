using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWebProjection.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebProjection.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;
        public HomeController(AppDbContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Personals.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Personal personal)
        {
            db.Personals.Add(personal);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id != null)
            {
                Personal personal = await db.Personals.FirstOrDefaultAsync(p => p.Id == id);
                if (personal != null)
                    return View(personal);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Personal personal = await db.Personals.FirstOrDefaultAsync(p => p.Id == id);
                if (personal != null)
                    return View(personal);
            }
            return NotFound();
        } 
        [HttpPost]
        public async Task<IActionResult> Edit(Personal personal)
        {
            db.Personals.Update(personal);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if(id != null)
            {
                Personal personal = await db.Personals.FirstOrDefaultAsync(p => p.Id == id);
                if(personal != null)
                {
                    return View(personal);
                }
                
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                Personal personal = new Personal { Id = id.Value };
                db.Entry(personal).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Search()
        {

            return View();
        }
        public async Task <IActionResult> GetResult(int id)
        {
            var res = await db.Personals.FindAsync(id);
            return View(res);
        }
        

    }
}
