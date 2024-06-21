namespace MKTDotNetCore.ConsoleAppRestClientExample.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public string? message { get; set; }
    }
}
