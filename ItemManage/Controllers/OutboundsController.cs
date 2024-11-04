using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemManage.Data;
using ItemManage.Models;

namespace ItemManage.Controllers
{
    public class OutboundsController : Controller
    {
        private readonly ItemManageContext _context;

        public OutboundsController(ItemManageContext context)
        {
            _context = context;
        }

        // GET: Outbounds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Outbound.ToListAsync());
        }

        public async Task<IActionResult> HIndex()
        {
            return View(await _context.Outbound.ToListAsync());
        }

        // GET: Outbounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outbound = await _context.Outbound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outbound == null)
            {
                return NotFound();
            }

            return View(outbound);
        }

        // GET: Outbounds/Create
        public IActionResult Create()
        {
            var itemCodes = _context.Item.Select(i => i.Code).ToList();
            ViewBag.ItemCodes = new SelectList(itemCodes);
            return View();
        }

        // POST: Outbounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemCode,ApplyDate,Quantity,UserId")] Outbound outbound)
        {
            ModelState.Remove("Status");
            if (ModelState.IsValid)
            {
                outbound.Status = "申请";
                _context.Add(outbound);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HIndex));
            }
            return View(outbound);
        }

        // GET: Outbounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outbound = await _context.Outbound.FindAsync(id);
            if (outbound == null)
            {
                return NotFound();
            }
            return View(outbound);
        }

        // POST: Outbounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemCode,ApplyDate,Quantity,UserId,Status")] Outbound outbound)
        {
            if (id != outbound.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outbound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutboundExists(outbound.Id))
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
            return View(outbound);
        }

        // GET: Outbounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outbound = await _context.Outbound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outbound == null)
            {
                return NotFound();
            }

            return View(outbound);
        }

        // POST: Outbounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outbound = await _context.Outbound.FindAsync(id);
            if (outbound != null)
            {
                _context.Outbound.Remove(outbound);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutboundExists(int id)
        {
            return _context.Outbound.Any(e => e.Id == id);
        }

        // 查看领用申请
        public async Task<IActionResult> Confirm()
        {
            var outbounds = await _context.Outbound.ToListAsync();
            return View(outbounds);
        }
        [HttpPost]
        // 确认领用申请
        public async Task<IActionResult> Confirm(int id)
        {
            Console.WriteLine(id);
            var outbound = await _context.Outbound.FindAsync(id);
            if (outbound == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FirstOrDefaultAsync(i => i.Code == outbound.ItemCode);
            if (item == null)
            {
                return NotFound();
            }
            var outbounds = await _context.Outbound.ToListAsync();
            if (item.Quantity < outbound.Quantity)
            {
                TempData["ErrorMessage"] = "库存不足，无法确认领用申请！";
                
                return View(outbounds);
            }
            
            // 扣减库存
            item.Quantity -= outbound.Quantity;
            outbound.Status = "确认";

            _context.Update(item);
            _context.Update(outbound);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "领用申请已确认，库存已更新！";
            outbounds = await _context.Outbound.ToListAsync();
            return View(outbounds);
        }
    }
}
