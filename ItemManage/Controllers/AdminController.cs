using Microsoft.AspNetCore.Mvc;

namespace ItemManage.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
