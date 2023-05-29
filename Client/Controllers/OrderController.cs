using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	[Authorize(Roles = "admin,user")]
	public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
