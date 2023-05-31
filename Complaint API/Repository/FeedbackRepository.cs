using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class FeedbackRepository : GeneralRepository<Feedback, int, MyContext>, IFeedbackRepository
    {
        private readonly IResolutionRepository _resolutionRepository;
        public FeedbackRepository(MyContext context, IResolutionRepository resolutionRepository) : base(context) {
            _resolutionRepository = resolutionRepository;
        }

        public async Task<IEnumerable<Feedback>> GetStaffFeedbackAsync(int id)
        {
            var resolutions = await _resolutionRepository.GetStaffResolutionAsync(id);
            var allFeedback = await GetAllAsync();
            var feedback = from r in resolutions
                           join f in allFeedback on r.Id equals f.ResolutionId
                           select f;
            return feedback;
        }
    }
}
