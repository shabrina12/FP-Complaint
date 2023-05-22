using Microsoft.AspNetCore.Mvc;
using Client.Repository.Interface;
using Client.Repository;
using Client.Models;

namespace Client.Controllers
{
    public class ComplaintController : Controller
    {
		private readonly IComplaintRepository repository;

		public ComplaintController(IComplaintRepository repository)
		{
			this.repository = repository;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
