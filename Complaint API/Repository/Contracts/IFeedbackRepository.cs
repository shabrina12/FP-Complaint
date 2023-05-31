using Complaint_API.Models;

namespace Complaint_API.Repository.Contracts
{
    public interface IFeedbackRepository : IGeneralRepository<Feedback, int>
    {
        Task<IEnumerable<Feedback>> GetStaffFeedbackAsync(int id);
    }
}
