using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly WebApplication1Context _context;

        public LoginController(WebApplication1Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //if (HttpContext.Session.GetString("NickName")==null)
            //{
            //    return Redirect("/Login/Index");
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Name == "admin" && model.Password == "0000")
            {
                return RedirectToAction("Index", "Admin");
            }

            
            var data = _context.Student.FirstOrDefault(p => p.UserId == model.Name);
            if (data == null)
            {
                ViewBag.notice = "用户不存在！";
            }
            else if (data.Password != model.Password)
            {
                ViewBag.notice = "密码错误！";
            }
            else
            {
                // 将用户ID存储在会话中
                HttpContext.Session.SetString("UserId", data.StudentID.ToString());
                HttpContext.Session.SetString("NickName", data.UserId);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public ActionResult LoginOut()
        {
            HttpContext.Session.SetString("NickName", "");
            return Redirect("/Login/Index");
        }
    }
}
