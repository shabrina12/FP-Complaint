using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class UserRoleRepository : GeneralRepository<UserRole, int, MyContext>, IUserRoleRepository
    {
        public UserRoleRepository(MyContext context) : base(context) { }
    }
}
