using Client.Models;
using Client.Repository.Interface;

namespace Client.Repository
{
    public class EmployeeRepository : GeneralRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(string request = "Employee/") : base(request)
        {

        }
    }
}
