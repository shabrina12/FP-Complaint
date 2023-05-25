using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
