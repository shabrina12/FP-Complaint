using Client.Repository.Interface;
using Client.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Client.Repository
{
	public class GeneralRepository<Entity, TId> : IGeneralRepository<Entity, TId>
		where Entity : class
	{
		private readonly string request;
		private readonly HttpClient httpClient;
		private readonly IHttpContextAccessor contextAccessor;

		public GeneralRepository(string request)
		{
			this.request = request;
			contextAccessor = new HttpContextAccessor();
			httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://localhost:7127/api/")
			};
		}

		public async Task<ResponseListVM<Entity>> Get()
		{
			ResponseListVM<Entity> entityVM = null;
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			using (var response = await httpClient.GetAsync(request))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entityVM = JsonConvert.DeserializeObject<ResponseListVM<Entity>>(apiResponse);
			}
			return entityVM;
		}

		public async Task<ResponseViewModel<Entity>> Get(TId id)
		{
			ResponseViewModel<Entity> entity = null;

			using (var response = await httpClient.GetAsync(request + id))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entity = JsonConvert.DeserializeObject<ResponseViewModel<Entity>>(apiResponse);
			}
			return entity;
		}

		public async Task<ResponseMessageVM> Post(Entity entity)
		{
			ResponseMessageVM entityVM = null;
			StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
			using (var response = httpClient.PostAsync(request, content).Result)
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
			}
			return entityVM;
		}

		public async Task<ResponseMessageVM> Put(TId id, Entity entity)
		{
			ResponseMessageVM entityVM = null;
			StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
			using (var response = httpClient.PutAsync(request + id, content).Result)
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
			}
			return entityVM;
		}

		public async Task<ResponseMessageVM> Delete(TId id)
		{
			ResponseMessageVM entityVM = null;
			StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
			using (var response = httpClient.DeleteAsync(request + id).Result)
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
			}
			return entityVM;
		}
	}
}
