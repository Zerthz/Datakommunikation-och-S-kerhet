namespace Uppgift3.Models
{
    public class ResponseModel
    {
        public float Id { get; set; }
        public float RecievedId { get; set; }
        public string? Message { get; set; }
        public bool IsClosing { get; set; } = false;
    }
}
