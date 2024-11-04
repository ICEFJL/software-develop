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
    public class ItemsController : Controller
    {
        private readonly ItemManageContext _context;

        public ItemsController(ItemManageContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Item.ToListAsync());
        }

        public async Task<IActionResult> Bounds()
        {
            return View(await _context.Item.ToListAsync());
        }

        public async Task<IActionResult> HBounds()
        {
            return View(await _context.Item.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Origin,Specification,Model,ImagePath")] Item item, string submitType, IFormFile img)
        {
            if (submitType == "Create")
            {
                ModelState.Remove("img");
                ModelState.Remove("Code");
                if (ModelState.IsValid)
                {
                    item.Quantity = 0;
                    _context.Add(item);
                    await _context.SaveChangesAsync();

                    // 获取插入后的自动生成的Id
                    item.Code = item.Id.ToString();

                    // 更新Code字段
                    _context.Update(item);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                var msg = string.Empty;
                foreach (var value in ModelState.Values)
                {
                    if (value.Errors.Count > 0)
                    {
                        foreach (var error in value.Errors)
                        {
                            msg = msg + error.ErrorMessage;
                        }
                    }
                }
                Console.WriteLine(msg);
            }
            else if (submitType == "Upload")
            {
                if (img == null || img.Length == 0)
                {
                    ViewData["Flag"] = "图片未选择！";
                    Console.WriteLine("图片未选择！");
                    return View(item);
                }
                // 获取当前时间的时间戳
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                // 将时间戳添加到文件名中
                var newFileName = $"{img.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                // 将图片的URL存储在ViewData中
                ViewData["ImageUrl"] = $"/images/{newFileName}";

                ViewData["Flag"] = "图片已经上传！";
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Category,Origin,Specification,Model,ImagePath")] Item item, string submitType, IFormFile img)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            if (submitType == "Save")
            {
                ModelState.Remove("img");
                if (ModelState.IsValid)
                {
                    try
                    {
                        var originalItem = await _context.Item.FindAsync(id);
                        if (originalItem == null)
                        {
                            return NotFound();
                        }

                        // 将item.Quantity设置为原始Item对象的Quantity值
                        item.Quantity = originalItem.Quantity;

                        // 将原始实体从上下文中分离
                _context.Entry(originalItem).State = EntityState.Detached;

                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemExists(item.Id))
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
            }
            else if (submitType == "Upload")
            {
                if (img == null || img.Length == 0)
                {
                    ViewData["Flag"] = "图片未选择！";
                    Console.WriteLine("图片未选择！");
                    return View(item);
                }
                // 获取当前时间的时间戳
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                // 将时间戳添加到文件名中
                var newFileName = $"{img.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                // 将图片的URL存储在ViewData中
                ViewData["ImageUrl"] = $"/images/{newFileName}";

                ViewData["Flag"] = "图片已经上传！";
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                _context.Item.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }

    }
}
