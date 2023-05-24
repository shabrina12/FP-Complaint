using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int ResolutionId { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}
