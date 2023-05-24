using Client.Models;
using Client.Repository.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repository
{
	public class RoleRepository : GeneralRepository<Role, int>, IRoleRepository
	{
        private readonly HttpClient httpClient;
        private readonly string request;
        public RoleRepository(string request = "Role/") : base(request)
		{
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7127/api/")
            };
            this.request = request;
        }
        public async Task<ResponseMessageVM> PostRole(RoleVM entity, string token)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }
    }
}
