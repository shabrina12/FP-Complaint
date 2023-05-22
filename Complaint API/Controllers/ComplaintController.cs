using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Complaint_API.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Complaint_API.Controllers
{
    [Authorize]
    public class ComplaintController : BaseController<IComplaintRepository, Complaint, int>
    {
        public ComplaintController(IComplaintRepository repository) : base(repository) { }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public override async Task<ActionResult> InsertAsync(Complaint entity)
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
        public async Task<ActionResult> InserComplainttAsync(ComplaintVM request)
        {
            Complaint entity = new Complaint
            {
                Title = request.Title,
                Description = request.Description,
                OrderId = request.OrderId,
                Status = 0,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            var result = await InsertAsync(entity);
            return result;
        }
    }
}
