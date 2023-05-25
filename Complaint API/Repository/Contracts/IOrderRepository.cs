using Complaint_API.Models;

namespace Complaint_API.Repository.Contracts
{
    public interface IOrderRepository : IGeneralRepository<Order, int>
    {
        public Task<IEnumerable<Order>> GetMyAsync(int id);
    }
}
