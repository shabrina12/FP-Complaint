using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class FeedbackRepository : GeneralRepository<Feedback, int, MyContext>, IFeedbackRepository
    {
        public FeedbackRepository(MyContext context) : base(context) { }
    }
}
