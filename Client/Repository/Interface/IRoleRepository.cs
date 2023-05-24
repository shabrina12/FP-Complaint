using Client.Models;
using Client.ViewModels;

namespace Client.Repository.Interface
{
	public interface IRoleRepository : IGeneralRepository<Role, int>
	{
        public Task<ResponseMessageVM> PostRole(RoleVM entity, string token);
    }
}
