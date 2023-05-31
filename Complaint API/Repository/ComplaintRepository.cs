using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels.Request;
using Microsoft.EntityFrameworkCore;

namespace Complaint_API.Repository
{
    public class ComplaintRepository : GeneralRepository<Complaint, int, MyContext>, IComplaintRepository
    {
        private readonly IOrderRepository _orderRepository;
        public ComplaintRepository(MyContext context, IOrderRepository orderRepository) : base(context) {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Complaint>> GetMyAsync(int id)
        {
            var complaints = await GetAllAsync();
            var orders = await _orderRepository.GetMyAsync(id);
            var complaint = from o in orders
                            join c in complaints on o.Id equals c.OrderId
                            select c;
            return complaint;
        }

        public async Task<int> ChangeStatusAsync(ChangeStatusVM request)
        {
            var complaint = await _context.Complaints.FindAsync(request.EntityId);
            if (complaint == null)
            {
                return 0;
            }
            complaint.Status = request.Status;
            complaint.DateUpdated = DateTime.Now;
            _context.Complaints.Update(complaint);
            return await _context.SaveChangesAsync();
        }
    }
}
