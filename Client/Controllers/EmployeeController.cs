using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	[Authorize(Roles = "admin")]
	public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
