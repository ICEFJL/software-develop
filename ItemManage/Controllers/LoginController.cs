using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemManage.Models;
using ItemManage.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ItemManage.Controllers
{
    public class LoginController : Controller
    {
        private readonly ItemManageContext _context;

        public LoginController(ItemManageContext context)
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

            
            var data = _context.User.FirstOrDefault(p => p.UserId == model.Name);
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
                HttpContext.Session.SetString("Name", data.Name);
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
