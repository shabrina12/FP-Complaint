namespace Client.ViewModels
{
	public class ResponseViewModel<Entity>
	{
		public string StatusCode { get; set; } = null!;
		public string Message { get; set; } = null!;
		public Entity Data { get; set; }
	}
}
