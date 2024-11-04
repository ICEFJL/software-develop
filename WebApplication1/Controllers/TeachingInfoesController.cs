using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeachingInfoesController : Controller
    {
        private readonly WebApplication1Context _context;

        public TeachingInfoesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: TeachingInfoes
        public async Task<IActionResult> Index()
        {
            // 获取所有不同的科目和地点
            var Subjects = _context.TeachingInfo.Select(i => i.Subject).Distinct().ToList();
            ViewBag.Subjects = new SelectList(Subjects);
            var Locations = _context.TeachingInfo.Select(i => i.Location).Distinct().ToList();
            ViewBag.Locations = new SelectList(Locations);
            return View(await _context.TeachingInfo.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string searchString, string subjectFilter, string locationFilter)
        {
            var query = _context.TeachingInfo.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(t => t.School.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(subjectFilter))
            {
                query = query.Where(t => t.Subject == subjectFilter);
            }

            if (!string.IsNullOrEmpty(locationFilter))
            {
                query = query.Where(t => t.Location == locationFilter);
            }

            var Subjects = _context.TeachingInfo.Select(i => i.Subject).Distinct().ToList();
            ViewBag.Subjects = new SelectList(Subjects);
            var Locations = _context.TeachingInfo.Select(i => i.Location).Distinct().ToList();
            ViewBag.Locations = new SelectList(Locations);

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> HIndex()
        {
            var Subjects = _context.TeachingInfo.Select(i => i.Subject).Distinct().ToList();
            ViewBag.Subjects = new SelectList(Subjects);
            var Locations = _context.TeachingInfo.Select(i => i.Location).Distinct().ToList();
            ViewBag.Locations = new SelectList(Locations);
            return View(await _context.TeachingInfo.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> HIndex(string searchString, string subjectFilter, string locationFilter)
        {
            var query = _context.TeachingInfo.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(t => t.School.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(subjectFilter))
            {
                query = query.Where(t => t.Subject == subjectFilter);
            }

            if (!string.IsNullOrEmpty(locationFilter))
            {
                query = query.Where(t => t.Location == locationFilter);
            }

            var Subjects = _context.TeachingInfo.Select(i => i.Subject).Distinct().ToList();
            ViewBag.Subjects = new SelectList(Subjects);
            var Locations = _context.TeachingInfo.Select(i => i.Location).Distinct().ToList();
            ViewBag.Locations = new SelectList(Locations);

            return View(await query.ToListAsync());
        }
        // GET: TeachingInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingInfo = await _context.TeachingInfo
                .FirstOrDefaultAsync(m => m.TeachID == id);
            if (teachingInfo == null)
            {
                return NotFound();
            }

            return View(teachingInfo);
        }

        // GET: TeachingInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeachingInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeachID,School,Location,StartDate,EndDate,Subject,mem,maxmem")] TeachingInfo teachingInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teachingInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teachingInfo);
        }

        // GET: TeachingInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingInfo = await _context.TeachingInfo.FindAsync(id);
            if (teachingInfo == null)
            {
                return NotFound();
            }
            return View(teachingInfo);
        }

        // POST: TeachingInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeachID,School,Location,StartDate,EndDate,Subject,mem,maxmem")] TeachingInfo teachingInfo)
        {
            if (id != teachingInfo.TeachID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachingInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachingInfoExists(teachingInfo.TeachID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teachingInfo);
        }

        // GET: TeachingInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingInfo = await _context.TeachingInfo
                .FirstOrDefaultAsync(m => m.TeachID == id);
            if (teachingInfo == null)
            {
                return NotFound();
            }

            return View(teachingInfo);
        }

        // POST: TeachingInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teachingInfo = await _context.TeachingInfo.FindAsync(id);
            if (teachingInfo != null)
            {
                _context.TeachingInfo.Remove(teachingInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeachingInfoExists(int id)
        {
            return _context.TeachingInfo.Any(e => e.TeachID == id);
        }
    }
}
