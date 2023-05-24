using Microsoft.AspNetCore.Mvc;
using Client.Repository.Interface;
using Client.Repository;
using Client.ViewModels;

namespace Client.Controllers
{
	public class RoleController : Controller
	{
        private readonly IRoleRepository repository;
        public RoleController(IRoleRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult AddRole()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleVM role)
        {
            string token = HttpContext.Session.GetString("JWToken");
            var result = await repository.PostRole(role, token);

            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.StatusCode == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
                return View();
            }
            else if (result.StatusCode == "200")
            {
                TempData["Success"] = $"Data has been successfully added! - {result.Message}!";
                return RedirectToAction("Index", "Role");
            }
            return View();
        }
    }
}
