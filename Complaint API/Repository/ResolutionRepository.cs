using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Complaint_API.Repository
{
    public class ResolutionRepository : GeneralRepository<Resolution, int, MyContext>, IResolutionRepository
    {
        private readonly IComplaintRepository _complaintRepository;
        public ResolutionRepository(MyContext context, IComplaintRepository complaintRepository) : base(context) {
            _complaintRepository = complaintRepository;
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
