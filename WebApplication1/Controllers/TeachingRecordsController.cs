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
    public class TeachingRecordsController : Controller
    {
        private readonly WebApplication1Context _context;

        public TeachingRecordsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: TeachingRecords
        public async Task<IActionResult> Index()
        {
            var webApplication1Context = _context.TeachingRecord.Include(t => t.Student).Include(t => t.TeachingInfo);
            return View(await webApplication1Context.ToListAsync());
        }

        // GET: TeachingRecords
        [HttpPost]
        public async Task<IActionResult> Index(string searchString, string statusFilter, string evaluationFilter)
        {
            var query = _context.TeachingRecord.Include(t => t.Student).Include(t => t.TeachingInfo).AsQueryable();
            int number;
            bool success = int.TryParse(searchString, out number);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(t => t.Student.UserId.Contains(searchString) || (success && t.TeachID == number));
            }

            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(t => t.Status == statusFilter);
            }

            if (!string.IsNullOrEmpty(evaluationFilter))
            {
                query = query.Where(t => t.Evaluation == evaluationFilter);
            }

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> HIndex()
        {
            // 从会话中获取当前用户的 UserId
            var userIdString = HttpContext.Session.GetString("NickName");
            if (string.IsNullOrEmpty(userIdString))
            {
                // 如果 UserId 不存在，重定向到登录页面
                return RedirectToAction("Index", "Login");
            }

            // 查询 TeachingRecord 并过滤 Student.UserId 等于当前用户的记录
            var webApplication1Context = _context.TeachingRecord
                .Include(t => t.Student)
                .Include(t => t.TeachingInfo)
                .Where(t => t.Student.UserId == userIdString);

            return View(await webApplication1Context.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> HIndex(string searchString, string statusFilter, string evaluationFilter)
        {
            // 从会话中获取当前用户的 UserId
            var userIdString = HttpContext.Session.GetString("NickName");
            if (string.IsNullOrEmpty(userIdString))
            {
                // 如果 UserId 不存在，重定向到登录页面
                return RedirectToAction("Index", "Login");
            }

            // 查询 TeachingRecord 并过滤 Student.UserId 等于当前用户的记录
            var query = _context.TeachingRecord.Include(t => t.Student).Include(t => t.TeachingInfo).AsQueryable().Where(t => t.Student.UserId == userIdString);
            int number;
            bool success = int.TryParse(searchString, out number);

            if (success && !string.IsNullOrEmpty(searchString))
            {
                query = query.Where(t => t.TeachID == number);
            }

            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(t => t.Status == statusFilter);
            }

            if (!string.IsNullOrEmpty(evaluationFilter))
            {
                query = query.Where(t => t.Evaluation == evaluationFilter);
            }

            return View(await query.ToListAsync());
        }
        // GET: TeachingRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingRecord = await _context.TeachingRecord
                .Include(t => t.Student)
                .Include(t => t.TeachingInfo)
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (teachingRecord == null)
            {
                return NotFound();
            }

            return View(teachingRecord);
        }

        // GET: TeachingRecords/Create
        public IActionResult Create()
        {
            ViewData["TeachID"] = new SelectList(_context.TeachingInfo, "TeachID", "TeachID");
            return View();
        }

        // POST: TeachingRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordID,TeachID,StudentID,Evaluation,Status")] TeachingRecord teachingRecord)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            int userId;
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out userId))
            {
                teachingRecord.StudentID = userId;
            }
            else
            {
                // 处理 UserId 不可用或无效的情况
                return RedirectToAction("Index", "Login");
            }
            
            // 检查关联的 Student 和 TeachingInfo 是否存在
            var studentExists = await _context.Student.FirstOrDefaultAsync(s => s.StudentID == teachingRecord.StudentID);
            var teachingInfoExists = await _context.TeachingInfo.FirstOrDefaultAsync(t => t.TeachID == teachingRecord.TeachID);

            if (studentExists==null || teachingInfoExists==null)
            {
                ModelState.AddModelError(string.Empty, "关联的学生或支教信息不存在。");
                ViewData["TeachID"] = new SelectList(_context.TeachingInfo, "TeachID", "School", teachingRecord.TeachID);
                return View(teachingRecord);
            }
            teachingRecord.Student = studentExists;
            teachingRecord.TeachingInfo= teachingInfoExists;
            ModelState.Remove("Student");
            ModelState.Remove("TeachingInfo");
            if (ModelState.IsValid)
            {
                _context.Add(teachingRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HIndex));
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

            ViewData["TeachID"] = new SelectList(_context.TeachingInfo, "TeachID", "TeachID", teachingRecord.TeachID);
            return View(teachingRecord);
        }

        // GET: TeachingRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingRecord = await _context.TeachingRecord.FindAsync(id);
            if (teachingRecord == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Gender", teachingRecord.StudentID);
            ViewData["TeachID"] = new SelectList(_context.TeachingInfo, "TeachID", "Location", teachingRecord.TeachID);
            return View(teachingRecord);
        }

        // POST: TeachingRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordID,TeachID,StudentID,Evaluation,Status")] TeachingRecord teachingRecord)
        {
            if (id != teachingRecord.RecordID)
            {
                return NotFound();
            }
            ModelState.Remove("Student");
            ModelState.Remove("TeachingInfo");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachingRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachingRecordExists(teachingRecord.RecordID))
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
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Gender", teachingRecord.StudentID);
            ViewData["TeachID"] = new SelectList(_context.TeachingInfo, "TeachID", "Location", teachingRecord.TeachID);
            return View(teachingRecord);
        }

        // GET: TeachingRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingRecord = await _context.TeachingRecord
                .Include(t => t.Student)
                .Include(t => t.TeachingInfo)
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (teachingRecord == null)
            {
                return NotFound();
            }

            return View(teachingRecord);
        }

        // POST: TeachingRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teachingRecord = await _context.TeachingRecord.FindAsync(id);
            if (teachingRecord != null)
            {
                if (teachingRecord.Status == "确认")
                {
                    // 获取对应的 TeachingInfo
                    var teachingInfo = await _context.TeachingInfo.FindAsync(teachingRecord.TeachID);
                    if (teachingInfo != null)
                    {
                        // 减少报名人数
                        teachingInfo.mem -= 1;
                        _context.TeachingInfo.Update(teachingInfo);
                    }
                }

                _context.TeachingRecord.Remove(teachingRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeachingRecordExists(int id)
        {
            return _context.TeachingRecord.Any(e => e.RecordID == id);
        }

        public async Task<IActionResult> Confirm()
        {
            var teachingRecords = await _context.TeachingRecord
               .Where(tr => tr.Status == "申请")
               .ToListAsync();
            return View(teachingRecords);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id, string submitType)
        {
            // 查找支教记录
            var teachingRecord = await _context.TeachingRecord
                .Include(tr => tr.TeachingInfo)
                .FirstOrDefaultAsync(tr => tr.RecordID == id);

            if (teachingRecord == null)
            {
                return NotFound();
            }
            // 查找支教活动信息
            var teachingInfo = teachingRecord.TeachingInfo;
            if (teachingInfo == null)
            {
                return NotFound();
            }
            var teachingRecords = await _context.TeachingRecord
               .Where(tr => tr.Status == "申请")
               .ToListAsync();
            if (submitType == "Confirm")
            {
                

                // 检查是否已达到最大报名人数
                if (teachingInfo.mem >= teachingInfo.maxmem)
                {
                    TempData["ErrorMessage"] = "报名人数已满，无法确认支教申请！";
                    return View(teachingRecords);
                }

                // 更新报名人数
                teachingInfo.mem += 1;
                teachingRecord.Status = "确认";

            }else if(submitType == "Reject")
            {
                teachingRecord.Status = "拒绝";
            }
            _context.Update(teachingInfo);
            _context.Update(teachingRecord);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "支教申请已处理！";
            teachingRecords = await _context.TeachingRecord
               .Where(tr => tr.Status == "申请")
               .ToListAsync();
            return View(teachingRecords);
        }
    }
}
