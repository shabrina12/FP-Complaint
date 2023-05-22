namespace Complaint_API.ViewModels.Request
{
    public class ComplaintVM
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int OrderId { get; set; }
    }
}
