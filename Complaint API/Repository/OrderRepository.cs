using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;

namespace Complaint_API.Repository
{
    public class OrderRepository : GeneralRepository<Order, int, MyContext>, IOrderRepository
    {
        public OrderRepository(MyContext context) : base(context)
        {
            
        }
    }
}
