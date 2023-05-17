namespace Complaint_API.ViewModels
{
    public class ResultFormat
    {
        public int StatusCode { get; set; } = 200;
        public string Status { get; set; } = "OK";
        public string Message { get; set; } = "Success";
        public dynamic? Data { get; set; }

        public void ChangeStatus(int statusCode, string status, string message)
        {
            StatusCode = statusCode; Status = status; Message = message;
        }
    }
}
