using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UserRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
