using Client.Models;
using Client.Repository.Interface;

namespace Client.Repository
{
    public class ComplaintRepository : GeneralRepository<Complaint, int>, IComplaintRepository
    {
        public ComplaintRepository(string request = "Complaint/") : base(request)
        {

        }
    }
}
