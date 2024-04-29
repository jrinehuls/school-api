namespace SchoolAPI.Models.DTOs
{
    public class ErrorResponse
    {
        public string? Message { get; set; } = null;
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
    }
}
