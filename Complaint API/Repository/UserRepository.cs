using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class UserRepository : GeneralRepository<User, int, MyContext>, IUserRepository
    {
        public UserRepository(MyContext context) : base(context) { }    
    }
}
