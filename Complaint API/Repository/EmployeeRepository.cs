using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels.Request;
using Complaint_API.ViewModels.Response;
using Microsoft.EntityFrameworkCore;

namespace Complaint_API.Repository
{
    public class EmployeeRepository : GeneralRepository<Employee, int, MyContext>, IEmployeeRepository
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        public EmployeeRepository(MyContext context, 
            IProfileRepository profileRepository,
            IUserRoleRepository userRoleRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository) : base(context)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
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

        public async Task<IEnumerable<EmployeeStaffVM>> GetAllStaffAsync()
        {
            var profiles = await _profileRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var userRoles = await _userRoleRepository.GetAllAsync();
            var roles = await _roleRepository.GetAllAsync();
            var employees = await GetAllAsync();
            var staff = from e in employees
                        join p in profiles on e.ProfileId equals p.Id
                        join u in users on p.Id equals u.ProfileId
                        join ur in userRoles on u.Id equals ur.UserId
                        join r in roles on ur.RoleId equals r.Id
                        where r.Name == "staff"
                        select new EmployeeStaffVM
                        {
                            Id = e.Id,
                            Name = p.FirstName + " " + p.LastName,
                        };
            return staff;
        }
    }
}
