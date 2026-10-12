namespace Api.Middlewares;

public record ApiResponse<T>(string Message, int Code, T Data);