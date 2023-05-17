using Complaint_API.Models;
using Complaint_API.ViewModels;

namespace Complaint_API.Repository.Contracts
{
    public interface IUserRepository : IGeneralRepository<User, int>
    {
        public Task RegisterAsync(RegisterVM register);
        public Task<bool> LoginAsync(LoginVM login);
    }
}
