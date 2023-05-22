using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	public class RoleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
