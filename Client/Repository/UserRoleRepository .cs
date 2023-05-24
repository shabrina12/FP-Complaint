using Client.Models;
using Client.Repository.Interface;

namespace Client.Repository
{
	public class UserRoleRepository : GeneralRepository<UserRole, int>, IUserRoleRepository
	{
        public UserRoleRepository(string request = "UserRole/") : base(request)
		{
        }  
    }
}
