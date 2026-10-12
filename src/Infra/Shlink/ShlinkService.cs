using System.Net.Http.Json;
using Application.Interface;
using Common.Exceptions;

namespace Infra.Shlink;

public class ShlinkService(HttpClient client) : IShlinkService
{
    public async Task<string> ShortenUrl(string longUrl)
    {
        var response = await client.PostAsJsonAsync(
            "rest/v3/short-urls",
            new { longUrl });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ShlinkResponse>();

        if (result is null)
            throw new BadRequestException("Shlink error.");

        return result.ShortUrl;
    }
}