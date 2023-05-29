using Client.Repository.Interface;
using Client.Repository;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    [Authorize(Roles = "admin")]
	public class UserController : Controller
	{
        public IActionResult Index()
        {
            return View();
        }
    }
}
