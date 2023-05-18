using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_API.Controllers
{
    public class FeedbackController : BaseController<IFeedbackRepository, Feedback, int>
    {
        public FeedbackController(IFeedbackRepository repository) : base(repository) { }
    }
}
