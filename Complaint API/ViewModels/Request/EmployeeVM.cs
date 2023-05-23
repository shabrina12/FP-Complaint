using Complaint_API.Models;

namespace Complaint_API.ViewModels.Request
{
    public class EmployeeVM : Employee
    {
        public string FullName { get; set; } = null!;
        public int Gender { get; set; }
    }
}