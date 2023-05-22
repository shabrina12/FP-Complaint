using Client.Models;
using Client.ViewModels;

namespace Client.Repository.Interface
{
    public interface IComplaintRepository : IGeneralRepository<Complaint, int>
    {
		public Task<ResponseMessageVM> PostComplaint(ComplaintVM entity, string token);
	}
}
