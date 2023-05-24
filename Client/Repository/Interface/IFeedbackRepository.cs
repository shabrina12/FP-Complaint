using Client.Models;
using Client.ViewModels;

namespace Client.Repository.Interface
{
    public interface IFeedbackRepository : IGeneralRepository<Feedback, int>
    {
        public Task<ResponseMessageVM> PostFeedback(FeedbackVM entity, string token);
    }
}
