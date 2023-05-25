using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
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
    }
}
