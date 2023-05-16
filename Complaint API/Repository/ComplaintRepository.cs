using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class ComplaintRepository : GeneralRepository<Complaint, int, MyContext>, IComplaintRepository
    {
        public ComplaintRepository(MyContext context) : base(context) { }
    }
}
