using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Complaint_API.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Complaint_API.Controllers
{
    [Authorize]
    public class ResolutionController : BaseController<IResolutionRepository, Resolution, int>
    {
        private readonly IUserRepository _userRepository;
        public ResolutionController(IResolutionRepository repository, IUserRepository userRepository) : base(repository) {
            _userRepository = userRepository;
        }

        [HttpGet]
        public override async Task<ActionResult> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (User.IsInRole("user"))
            {
                results = await _repository.GetMyAsync(user.Id);
            }
            else if (User.IsInRole("staff"))
            {
                results = await _repository.GetStaffResolutionAsync(user.Id);
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public override async Task<ActionResult> InsertAsync(Resolution entity)
        {
            var result = await _repository.InsertAsync(entity);
            if (result is null)
            {
                return Conflict(new
                {
                    statusCode = HttpStatusCode.Conflict,
                    message = "Data Fail to Insert!"
                });
            }
            return new ObjectResult(new ResultFormat
            {
                StatusCode = 201,
                Status = "Success",
                Message = "Data Saved Successfully!",
                Data = result
            })
            {
                StatusCode = 201
            };
        }

        [HttpPost]
        public async Task<ActionResult> InsertAsynct(ResolutionVM request)
        {
            Resolution entity = new Resolution
            {
                EmployeeId = request.EmployeeId,
                ComplaintId = request.ComplaintId,
                Description = "",
                Status = null,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };
            return await InsertAsync(entity);
        }

        [HttpPost("changestatus")]
        public async Task<ActionResult> ChangeStatusAsync(ChangeStatusVM request)
        {
            int result = await _repository.ChangeStatusAsync(request);
            var resultFormat = new ResultFormat { Data = result };
            if (result == 0)
            {
                resultFormat.ChangeStatus(StatusCodes.Status404NotFound, "Not Found", "Complaint ID not found");
                return NotFound(resultFormat);
            }
            return Ok(resultFormat);
        }
    }
}
