namespace FartiksPlatform.BuildingBlocks.Errors;

public class ErrorDto
{
    public string Title { get; set; } = null!;
    public int Status { get; set; }
    public string ErrorType { get; set; } = null!;
    public string? Detail { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string TraceId { get; set; } = null!;
    public Dictionary<string, string[]> ValidationErrors { get; set; } = new();
}
