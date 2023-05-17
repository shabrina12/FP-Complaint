using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Complaint_API.Repository
{
    public class UserRoleRepository : GeneralRepository<UserRole, int, MyContext>, IUserRoleRepository
    {
        public UserRoleRepository(MyContext context) : base(context) { }

        public async Task<IEnumerable<string>> GetRolesByEmail(string email)
        {
            var accountRoles = await GetAllAsync();
            var users = await _context.Users.ToListAsync();
            var roles = await _context.Roles.ToListAsync();

            var getRoleByEmail = from ar in accountRoles
                                 join u in users on ar.UserId equals u.Id
                                 join r in roles on ar.RoleId equals r.Id
                                 where u.Email == email
                                 select r.Name;
            return getRoleByEmail.ToList();
        }
    }
}
