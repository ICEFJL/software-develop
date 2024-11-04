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
    public class InboundsController : Controller
    {
        private readonly ItemManageContext _context;

        public InboundsController(ItemManageContext context)
        {
            _context = context;
        }

        // GET: Inbounds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inbound.ToListAsync());
        }

        // GET: Inbounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbound = await _context.Inbound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inbound == null)
            {
                return NotFound();
            }

            return View(inbound);
        }

        // GET: Inbounds/Create
        public IActionResult Create()
        {
            var itemCodes = _context.Item.Select(i => i.Code).ToList();
            ViewBag.ItemCodes = new SelectList(itemCodes);
            return View();
        }

        // POST: Inbounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemCode,PurchaseDate,Quantity,UnitPrice")] Inbound inbound)
        {
            // 服务器端计算TotalPrice
            
            if (ModelState.IsValid)
            {
                inbound.TotalPrice = inbound.Quantity * inbound.UnitPrice;
                // 查找对应的物品
                var item = await _context.Item.FirstOrDefaultAsync(i => i.Code == inbound.ItemCode);

                // 更新物品数量
                item.Quantity += inbound.Quantity;

                // 保存入库信息和更新后的物品信息
                _context.Add(inbound);
                _context.Update(item);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "创建成功！";
            }
            var itemCodes = _context.Item.Select(i => i.Code).ToList();
            ViewBag.ItemCodes = new SelectList(itemCodes);
            return View(inbound);
        }

        // GET: Inbounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbound = await _context.Inbound.FindAsync(id);
            if (inbound == null)
            {
                return NotFound();
            }
            return View(inbound);
        }

        // POST: Inbounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemCode,PurchaseDate,Quantity,UnitPrice,TotalPrice")] Inbound inbound)
        {
            if (id != inbound.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inbound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InboundExists(inbound.Id))
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
            return View(inbound);
        }

        // GET: Inbounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbound = await _context.Inbound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inbound == null)
            {
                return NotFound();
            }

            return View(inbound);
        }

        // POST: Inbounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inbound = await _context.Inbound.FindAsync(id);
            if (inbound != null)
            {
                _context.Inbound.Remove(inbound);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InboundExists(int id)
        {
            return _context.Inbound.Any(e => e.Id == id);
        }
    }
}
