using Microsoft.AspNetCore.Mvc;
using Client.Repository.Interface;
using Client.Repository;
using Client.ViewModels;


namespace Client.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository repository;

        public FeedbackController(IFeedbackRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddFeedback()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFeedback(FeedbackVM feedback)
        {
            string token = HttpContext.Session.GetString("JWToken");
            var result = await repository.PostFeedback(feedback, token);

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
                return RedirectToAction("Index", "Feedback");
            }
            return View();
        }
    }
}
