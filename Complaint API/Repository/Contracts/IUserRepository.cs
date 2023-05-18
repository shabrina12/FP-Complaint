using Complaint_API.Models;
using Complaint_API.ViewModels;
using System.Collections;

namespace Complaint_API.Repository.Contracts
{
    public interface IUserRepository : IGeneralRepository<User, int>
    {
        public Task<IEnumerable> GetAllUserDataAsync();
        public Task RegisterAsync(RegisterVM register);
        public Task ChangePassword(LoginVM register);
        public Task<bool> LoginAsync(LoginVM login);
    }
}
