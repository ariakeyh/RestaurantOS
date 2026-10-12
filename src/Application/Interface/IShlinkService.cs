namespace Application.Interface;

public interface IShlinkService
{
    Task<string> ShortenUrl(string longUrl);
}