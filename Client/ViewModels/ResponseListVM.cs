namespace Client.ViewModels
{
	public class ResponseListVM<Entity>
	{
		public string StatusCode { get; set; } = null!;
		public string Message { get; set; } = null!;
		public List<Entity>? Data { get; set; }
	}
}
