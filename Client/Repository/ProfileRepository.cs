using Client.Models;
using Client.Repository.Interface;

namespace Client.Repository
{
    public class ProfileRepository : GeneralRepository<Profile, int>, IProfileRepository
    {
        public ProfileRepository(string request = "Profile/") : base(request)
        {

        }
    }
}
