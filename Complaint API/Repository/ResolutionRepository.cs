using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class ResolutionRepository : GeneralRepository<Resolution, int, MyContext>, IResolutionRepository
    {
        public ResolutionRepository(MyContext context) : base(context) { }
    }
}
