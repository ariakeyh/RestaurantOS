using System.Collections.Concurrent;

namespace Api.Services;

public class ConnectedUsersService
{
    private readonly ConcurrentDictionary<string, int> _connectedClients = new();

    public void AddUser(string connectionId, int userId)
    {
        _connectedClients[connectionId] = userId;
    }

    public void RemoveUser(string connectionId)
    {
        _connectedClients.TryRemove(connectionId, out _);
    }

    public int GetUserId(string connectionId)
    {
        return _connectedClients[connectionId];
    }
    
    public bool IsSystemOnline()
    {
        return !_connectedClients.IsEmpty;
    }
}