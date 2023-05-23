using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels.Request;

namespace Complaint_API.Repository
{
    public class EmployeeRepository : GeneralRepository<Employee, int, MyContext>, IEmployeeRepository
    {
        private readonly IProfileRepository _profileRepository;
        public EmployeeRepository(MyContext context, IProfileRepository profileRepository) : base(context)
        {
            _profileRepository = profileRepository;
        }
        public async Task<IEnumerable<EmployeeVM>> GetFullNameFromProfile()
        {
            var getProfile = await _profileRepository.GetAllAsync();
            var getEmployee = await GetAllAsync();
            var employeeProfile = from e in getEmployee
                                 join p in getProfile on e.ProfileId equals p.Id
                                 select new EmployeeVM
                                 {
                                     Id = e.Id,
                                     ProfileId = e.ProfileId,
                                     HiringDate = e.HiringDate,
                                     FullName = string.Concat(p.FirstName, " ", p.LastName),
                                     Gender = p.Gender
                                 };

            return employeeProfile;
        }
    }
}
