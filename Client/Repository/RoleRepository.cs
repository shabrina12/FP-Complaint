using Client.Models;
using Client.Repository.Interface;

namespace Client.Repository
{
	public class RoleRepository : GeneralRepository<Role, int>, IRoleRepository
	{
		public RoleRepository(string request = "Role/") : base(request)
		{

		}
	}
}
