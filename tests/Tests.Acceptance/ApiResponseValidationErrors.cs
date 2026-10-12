namespace Tests.Acceptance;

public class ApiResponseWithValidationErrors
{
    public string Message { get; set; }
    public int Code { get; set; }
    public Data Data { get; set; }
}

public class Data
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }
    public string TraceId { get; set; }
}