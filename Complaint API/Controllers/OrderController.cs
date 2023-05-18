using Complaint_API.Base;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_API.Controllers
{
    [Authorize]
    public class OrderController : BaseController<IOrderRepository, Order, int>
    {
        public OrderController(IOrderRepository repository) : base(repository)
        {
            
        }
    }
}
