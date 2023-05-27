using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Complaint_API.Controllers
{
    [Authorize(Roles = "admin")]
    public class EmployeeController : BaseController<IEmployeeRepository, Employee, int>
    {
        public EmployeeController(IEmployeeRepository repository) : base(repository) { }

        [HttpGet("Master")]
        public async Task<ActionResult> EmployeeData()
        {
            try
            {
                var results = await _repository.GetFullNameFromProfile();
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    Message = results
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("staff")]
        public async Task<ActionResult> StaffData()
        {
            var results = await _repository.GetAllStaffAsync();
            return Ok(new ResultFormat
            {
                Data = results
            });
        }
    }
}
