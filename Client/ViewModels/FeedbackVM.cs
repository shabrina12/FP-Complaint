using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class FeedbackVM
    {
        public int ResolutionId { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}
