using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class ProfileRepository : GeneralRepository<Profile, int, MyContext>, IProfileRepository
    {
        public ProfileRepository(MyContext context) : base(context) { }
    }
}
