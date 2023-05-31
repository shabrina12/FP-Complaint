using Complaint_API.Models;
using Complaint_API.ViewModels.Request;

namespace Complaint_API.Repository.Contracts
{
    public interface IResolutionRepository : IGeneralRepository<Resolution, int>
    {
        Task<IEnumerable<Resolution>> GetMyAsync(int id);
        Task<IEnumerable<Resolution>> GetStaffResolutionAsync(int id);
        Task<int> ChangeStatusAsync(ChangeStatusVM request);
    }
}
