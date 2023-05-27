using Complaint_API.Models;

namespace Complaint_API.Repository.Contracts
{
    public interface IResolutionRepository : IGeneralRepository<Resolution, int>
    {
        Task<IEnumerable<Resolution>> GetMyAsync(int id);
    }
}
