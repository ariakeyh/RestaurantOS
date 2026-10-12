namespace Application.Interface;

public interface ICacheService
{
    public Task<T?> GetValue<T>(string key);
    public Task SetValue<T>(string key, T value, TimeSpan? expiry = null);
    public Task<bool> Exists(string key);
    
    public Task<bool> Delete(string key);
}