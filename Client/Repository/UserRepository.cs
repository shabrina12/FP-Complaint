using Client.Models;
using Client.Repository.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Text;
using System.Text.Unicode;

namespace Client.Repository
{
	public class UserRepository : GeneralRepository<User, int>, IUserRepository
	{
		private readonly HttpClient httpClient;
		private readonly string request;

		public UserRepository(string request = "Auth/") : base(request)
		{
			httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://localhost:7127/api/")
			};
			this.request = request;
		}

		public async Task<ResponseViewModel<string>> Login(LoginVM entity)
		{
			ResponseViewModel<string> entityVM = null;
			var jsonString = JsonConvert.SerializeObject(entity);
			StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

			using (var response = httpClient.PostAsync(request + "Login", content).Result)
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entityVM = JsonConvert.DeserializeObject<ResponseViewModel<string>>(apiResponse);
			}
			return entityVM;
		}

		public async Task<ResponseMessageVM> Register(RegisterVM entity)
		{
			ResponseMessageVM entityVM = null;
			StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
			using (var response = httpClient.PostAsync(request + "Register", content).Result)
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
			}
			return entityVM;
		}
	}
}
