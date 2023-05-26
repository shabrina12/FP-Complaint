using Microsoft.AspNetCore.Mvc;
using Client.Repository.Interface;
using Client.Repository;
using Client.ViewModels;

namespace Client.Controllers
{
    public class ResolutionController : Controller
    {
        private readonly IResolutionRepository repository;

        public ResolutionController(IResolutionRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddResolution()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddResolution(ResolutionVM resolution)
        {
            string token = HttpContext.Session.GetString("JWToken");
            var result = await repository.PostResolution(resolution, token);

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
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "Complaint");
            }
            return View();
        }
    }
}
