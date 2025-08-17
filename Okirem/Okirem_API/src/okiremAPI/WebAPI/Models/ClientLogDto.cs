namespace WebAPI.Models;

public class ClientLogDto
{
    public string Level { get; set; } = "info";
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
