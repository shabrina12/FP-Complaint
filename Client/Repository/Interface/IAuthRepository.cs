using Client.Models;
using Client.ViewModels;

namespace Client.Repository.Interface
{
	public interface IAuthRepository : IGeneralRepository<User, int>
	{
		public Task<ResponseViewModel<string>> Login(LoginVM entity);
		public Task<ResponseMessageVM> Register(RegisterVM entity);
	}
}
