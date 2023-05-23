using Complaint_API.Handlers;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Complaint_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            ITokenService tokenService
        )
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResultFormat>> LoginAsync(LoginVM login)
        {
            bool result = await _userRepository.LoginAsync(login);
            if (!result)
            {
                return NotFound(new ResultFormat
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Status = "Not found",
                    Message = "Wrong Email and Password Combination",
                    Data = 0
                });
            }

            var user = await _userRepository.GetUserByEmailAsync(login.Email);
            string fullName = user.Profile.FirstName + " " + user.Profile.LastName;
            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, login.Email),
                        new Claim(ClaimTypes.Name, fullName),
                    };

            var getRoles = await _userRoleRepository.GetRolesByEmail(login.Email);
            foreach (var item in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var accessToken = _tokenService.GenerateAccessToken(claims);

            return Ok(new ResultFormat
            {
                Message = "Login Success",
                Data = accessToken
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterVM register)
        {
            try
            {
                await _userRepository.RegisterAsync(register);
                return Ok(new ResultFormat
                {
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Added User Successfully!",
                    Data = 1
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
