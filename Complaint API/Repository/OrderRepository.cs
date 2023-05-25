using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class OrderRepository : GeneralRepository<Order, int, MyContext>, IOrderRepository
    {
        public OrderRepository(MyContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetMyAsync(int id)
        {
            var orders = await GetAllAsync();
            return orders.Where(o => o.CustomerId == id);
        }
    }
}
