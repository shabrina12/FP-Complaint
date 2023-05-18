using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Complaint_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController<IUserRepository, User, int>
    {
        public UserController(IUserRepository repository) : base(repository) { }

        [HttpGet]
        public override async Task<ActionResult> GetAllAsync()
        {
            var results = await _repository.GetAllUserDataAsync();

            return Ok(new ResultFormat
            {
                Data = results
            });
        }

        [HttpPut("changepassword")]
        public async Task<ActionResult> ChangePassword(LoginVM change)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            if(email != change.Email)
            {
                return Forbid();
            }

            try
            {
                await _repository.ChangePassword(change);
                return Ok(new ResultFormat
                {
                    Message = "Password Changed Successfully!",
                    Data = 1
                });
            } catch
            {
                return BadRequest();
            }
        }
    }
}
