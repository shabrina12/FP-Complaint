﻿using Complaint_API.Models;

namespace Complaint_API.Repository.Contracts
{
    public interface IUserRoleRepository : IGeneralRepository<UserRole, int>
    {
        Task<IEnumerable<string>> GetRolesByEmail(string email);
    }
}
