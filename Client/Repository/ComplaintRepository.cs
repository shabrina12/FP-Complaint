using Client.Models;
using Client.Repository.Interface;
using Client.ViewModels;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repository
{
    public class ComplaintRepository : GeneralRepository<Complaint, int>, IComplaintRepository
    {
		private readonly HttpClient httpClient;
		private readonly string request;

		public ComplaintRepository(string request = "Complaint/") : base(request)
		{
			httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://localhost:7127/api/")
			};
			this.request = request;
		}
		public async Task<ResponseMessageVM> PostComplaint(ComplaintVM entity, string token)
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
