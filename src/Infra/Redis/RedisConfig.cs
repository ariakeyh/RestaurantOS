namespace Infra.Redis;

public class RedisConfig
{
    public string ConnectionString { get; set; }
    public string KeyPrefix { get; set; }
}