using Complaint_API.Contexts;
using Complaint_API.Handlers;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Complaint_API.Repository
{
    public class UserRepository : GeneralRepository<User, int, MyContext>, IUserRepository
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRepository(MyContext context,
            IProfileRepository profileRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository) : base(context) 
        {
            _profileRepository = profileRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable> GetAllUserDataAsync()
        {
            var users = await base.GetAllAsync();
            var usersProfile = await _profileRepository.GetAllAsync();
            var usersData = from u in users
                               join p in usersProfile on u.ProfileId equals p.Id
                               join ur in _context.UserRoles on u.Id equals ur.UserId
                               join r in _context.Roles on ur.RoleId equals r.Id
                               select new
                               {
                                   p.Id,
                                   Name = p.FirstName + ' ' + p.LastName,
                                   u.Email,
                                   p.Gender,
                                   p.BirthDate,
                                   p.PhoneNumber,
                                   Role = new
                                   {
                                       ur.Id,
                                       r.Name
                                   }
                               };
            return usersData;
        }

        public async Task RegisterAsync(RegisterVM register)
        {
            Profile profile = new Profile
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Gender = register.Gender,
                BirthDate = register.Birthdate,
                PhoneNumber = register.PhoneNumber,
            };
            await _profileRepository.InsertAsync(profile);

            User user = new User
            {
                Email = register.Email,
                Password = Hashing.HashPassword(register.Password),
                ProfileId = profile.Id,
            };
            await InsertAsync(user);

            UserRole userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = 2
            };
            await _userRoleRepository.InsertAsync(userRole);

            await _context.SaveChangesAsync();
        }

        public async Task ChangePassword(LoginVM register)
        {
            var users = await GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == register.Email);
            user!.Password = Hashing.HashPassword(register.Password);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> LoginAsync(LoginVM login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                return false;
            }
            return Hashing.ValidatePassword(login.Password, user.Password);
        }
    }
}
