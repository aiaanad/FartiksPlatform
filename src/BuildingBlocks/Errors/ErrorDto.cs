namespace BuildingBlocks.Errors;

public class ErrorDto
{
    public string Type { get; set; } = "https://tools.ietf.org/html/rfc7807";
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Detail { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public Dictionary<string, string[]> Errors { get; set; } = new();
}
