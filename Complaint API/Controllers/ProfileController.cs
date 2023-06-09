﻿using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Complaint_API.Controllers
{
    [Authorize]
    public class ProfileController : BaseController<IProfileRepository, Profile, int>
    {
        public ProfileController(IProfileRepository repository) : base(repository) { }
    }
}
