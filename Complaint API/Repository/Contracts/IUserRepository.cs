using Complaint_API.Models;
using Complaint_API.ViewModels;
using System.Collections;

namespace Complaint_API.Repository.Contracts
{
    public interface IUserRepository : IGeneralRepository<User, int>
    {
        Task<IEnumerable> GetAllUserDataAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task RegisterAsync(RegisterVM register);
        Task ChangePassword(LoginVM register);
        Task<bool> LoginAsync(LoginVM login);
    }
}
