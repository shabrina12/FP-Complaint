using Complaint_API.Contexts;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Complaint_API.Repository
{
    public class ResolutionRepository : GeneralRepository<Resolution, int, MyContext>, IResolutionRepository
    {
        public ResolutionRepository(MyContext context) : base(context) { }

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
