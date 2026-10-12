using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VaultSharp;
using VaultSharp.V1.AuthMethods.AppRole;

namespace Application.Vault;

public class VaultProvider
{
    private readonly VaultClient _vaultClient;
    private readonly ILogger<VaultProvider> _logger;
    private readonly string _appName;
    private readonly string _environmentName;

    public VaultProvider(IConfiguration configuration, ILogger<VaultProvider> logger, IHostEnvironment env)
    {
        _logger = logger;

        _appName = configuration["Vault:AppName"] ?? env.ApplicationName;
        _environmentName = env.EnvironmentName.ToLower();

        var vaultAddress = configuration["Vault:Address"];
        var vaultRoleId = configuration["Vault:RoleId"];
        var vaultSecretId = configuration["Vault:SecretId"];

        var authMethod = new AppRoleAuthMethodInfo(vaultRoleId, vaultSecretId);
        var settings = new VaultClientSettings(vaultAddress, authMethod);

        _vaultClient = new VaultClient(settings);

        _logger.LogInformation("VaultProvider initialized for {AppName} in {Environment}", _appName, _environmentName);
    }

    public async Task<TData> FetchKV<TData>()
    {
        var key= typeof(TData).Name.TrimEnd("Config".ToArray()).ToLower();
        var fullPath = $"{_appName}/{_environmentName}/{key}";

        _logger.LogInformation("Fetching secret from Vault: {Path}", fullPath);

        var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(
            path: fullPath,
            mountPoint: "kv"
        );

        if (secret?.Data?.Data == null || secret.Data.Data.Count == 0)
        {
            throw new VaultNotFound($"Vault secret at '{fullPath}' is empty or missing.");
        }

        var json = JsonSerializer.Serialize(secret.Data.Data);
        var result = JsonSerializer.Deserialize<TData>(json);

        if (result is null)
        {
            throw new VaultNotFound(
                $"Failed to deserialize Vault secret at '{fullPath}' into {typeof(TData).Name}.");
        }

        _logger.LogInformation("Successfully fetched secret from {Path}", fullPath);

        return result;
    }
}