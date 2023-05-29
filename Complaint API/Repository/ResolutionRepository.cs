using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Complaint_API.Repository
{
    public class ResolutionRepository : GeneralRepository<Resolution, int, MyContext>, IResolutionRepository
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public ResolutionRepository(MyContext context, 
            IComplaintRepository complaintRepository,
            IProfileRepository profileRepository,
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository) : base(context) {
            _complaintRepository = complaintRepository;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<Resolution>> GetMyAsync(int id)
        {
            var resolutions = await GetAllAsync();
            var complaints = await _complaintRepository.GetMyAsync(id);
            var resolution = from c in complaints
                             join r in resolutions on c.Id equals r.ComplaintId
                            select r;
            return resolution;
        }

        public async Task<IEnumerable<Resolution>> GetStaffResolutionAsync(int id)
        {
            var users = await _userRepository.GetAllAsync();
            var profiles = await _profileRepository.GetAllAsync();
            var employees = await _employeeRepository.GetAllAsync();
            var allResolutions = await GetAllAsync();
            var resolution = from e in employees
                             join p in profiles on e.ProfileId equals p.Id
                             join u in users on p.Id equals u.ProfileId
                             join re in allResolutions on e.Id equals re.EmployeeId
                             where u.Id == id
                             select re;
            return resolution;
        }

        public override async Task<Resolution?> InsertAsync(Resolution entity)
        {
            await _context.Set<Resolution>().AddAsync(entity);

            var complaint = await _context.Complaints.FirstOrDefaultAsync(c => c.Id == entity.ComplaintId);
            complaint!.Status = 1;
            _context.Complaints.Update(complaint);

            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
