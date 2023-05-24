using Client.Repository.Interface;
using Client.Repository;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	public class UserController : Controller
	{
        public IActionResult Index()
        {
            return View();
        }
    }
}
