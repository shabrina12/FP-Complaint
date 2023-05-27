using Complaint_API.Models;
using Complaint_API.ViewModels.Request;
using Complaint_API.ViewModels.Response;

namespace Complaint_API.Repository.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, int>
    {
        Task<IEnumerable<EmployeeVM>> GetFullNameFromProfile();
        Task<IEnumerable<EmployeeStaffVM>> GetAllStaffAsync();
    }
}
