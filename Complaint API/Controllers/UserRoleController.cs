using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_API.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserRoleController : BaseController<IUserRoleRepository, UserRole, int>
    {
        public UserRoleController(IUserRoleRepository repository) : base(repository)
        {
            
        }
    }
}
