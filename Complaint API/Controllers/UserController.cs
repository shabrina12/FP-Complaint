using Complaint_API.Base;
using Complaint_API.Handlers;
using Complaint_API.Models;
using Complaint_API.Repository.Contracts;
using Complaint_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Complaint_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : BaseController<IUserRepository, User, int>
    {
        private IUserRoleRepository _userRoleRepository;
        private readonly ITokenService _tokenService;

        public UserController(
            IUserRepository repository,
            IUserRoleRepository userRoleRepository,
            ITokenService tokenService) : base(repository)
        {
            _userRoleRepository = userRoleRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResultFormat>> LoginAsync(LoginVM login)
        {
            bool result = await _repository.LoginAsync(login);
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

            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, login.Email),
                        new Claim(ClaimTypes.Name, login.Email),
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
                await _repository.RegisterAsync(register);
                return Ok(new ResultFormat
                {
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Added User Successfully!",
                    Data = 1
                });
            } catch
            {
                return BadRequest();
            }
        }
    }
}
