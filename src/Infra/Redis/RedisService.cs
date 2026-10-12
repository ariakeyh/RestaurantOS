using System.Text.Json;
using Application.Interface;
using StackExchange.Redis;

namespace Infra.Redis;

public class CacheService(
    IConnectionMultiplexer connectionMultiplexer,
    RedisConfig redisConfig)
    : ICacheService
{
    private readonly IDatabase _db = connectionMultiplexer.GetDatabase();
    private readonly string _prefix = redisConfig.KeyPrefix.TrimEnd(':');

    private string BuildKey(string key)
    {
        return string.IsNullOrWhiteSpace(_prefix) ? key : $"{_prefix}:{key}";
    }

    public async Task<T?> GetValue<T>(string key)
    {
        var redisKey = BuildKey(key);
        var value = await _db.StringGetAsync(redisKey);

        return value.IsNullOrEmpty
            ? default
            : JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetValue<T>(string key, T value, TimeSpan? expiry = null)
    {
        var redisKey = BuildKey(key);
        var serialized = JsonSerializer.Serialize(value);

        await _db.StringSetAsync(redisKey, serialized, expiry);
    }

    public async Task<bool> Exists(string key)
    {
        var redisKey = BuildKey(key);
        return await _db.KeyExistsAsync(redisKey);
    }

    public async Task<bool> Delete(string key)
    {
        var redisKey = BuildKey(key);
        return await _db.KeyDeleteAsync(redisKey);
    }
}