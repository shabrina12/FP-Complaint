using Complaint_API.Models;

namespace Complaint_API.Repository.Contracts
{
    public interface IOrderRepository : IGeneralRepository<Order, int>
    {
        Task<IEnumerable<Order>> GetMyAsync(int id);
    }
}
