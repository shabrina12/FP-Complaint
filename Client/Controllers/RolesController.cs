using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	public class RolesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
