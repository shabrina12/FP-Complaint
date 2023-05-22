﻿using Microsoft.AspNetCore.Mvc;
using Client.Repository.Interface;
using Client.Repository;
using Client.ViewModels;

namespace Client.Controllers
{
    public class ComplaintController : Controller
    {
		private readonly IComplaintRepository repository;

		public ComplaintController(IComplaintRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public IActionResult AddComplaint()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddComplaint(ComplaintVM complaint)
		{
			string token = HttpContext.Session.GetString("JWToken");
			var result = await repository.PostComplaint(complaint, token);
			
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
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
	}
}
