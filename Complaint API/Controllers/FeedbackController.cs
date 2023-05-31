using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Complaint_API.Controllers
{
    [Authorize]
    public class FeedbackController : BaseController<IFeedbackRepository, Feedback, int>
    {
        private readonly IUserRepository _userRepository;
        public FeedbackController(IFeedbackRepository repository, IUserRepository userRepository) : base(repository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public override async Task<ActionResult> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (User.IsInRole("staff"))
            {
                results = await _repository.GetStaffFeedbackAsync(user.Id);
            }
            if (results.Count() is 0)
            {
                return Ok(new
                {
                    statusCode = 404,
                    message = "Data Not Found!",
                    data = results
                });
            }

            return Ok(new ResultFormat
            {
                Data = results
            });
        }
    }
}
