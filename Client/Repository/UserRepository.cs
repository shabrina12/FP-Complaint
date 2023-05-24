using Client.Models;
using Client.Repository.Interface;

namespace Client.Repository
{
	public class UserRepository : GeneralRepository<User, int>, IUserRepository
	{
        public UserRepository(string request = "User/") : base(request)
        {

        }
    }
}
