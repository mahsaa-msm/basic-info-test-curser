namespace Master.Data.Core.RequestResponse.Common.Requests;

public class FailResponse
{
    public string Type { get; set; } = default!;
    public string Title { get; set; } = default!;
    public int Status { get; set; }
    public string TraceId { get; set; } = default!;
    public Dictionary<string, string[]> Errors { get; set; }
}