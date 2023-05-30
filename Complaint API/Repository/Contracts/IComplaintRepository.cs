using Complaint_API.Models;
using Complaint_API.ViewModels.Request;

namespace Complaint_API.Repository.Contracts
{
    public interface IComplaintRepository : IGeneralRepository<Complaint, int>
    {
        Task<IEnumerable<Complaint>> GetMyAsync(int id);
        Task<int> ChangeStatusAsync(ComplaintChangeStatusVM request);
    }
}
