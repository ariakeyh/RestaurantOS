using Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class NotifierHub(
    ConnectedUsersService connectedUsersService,
    ILogger<NotifierHub> logger) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var user = Context.User;
        if (user is { Identity.IsAuthenticated: false })
        {
            Context.Abort();
            throw new UnauthorizedAccessException();
        }
        
        var connectedUserId = int.Parse(user.Identity.Name);
        connectedUsersService.AddUser(Context.ConnectionId, connectedUserId);

        LogState(connectedUserId, "Connected");

        await base.OnConnectedAsync();
    }
    

    private void LogState(int connectedUserId, string state)
    {
        logger.LogInformation("User with id :{ConnectedUserId} {SignalRState} with Connection ID: {SignalRConnectionId}",
            connectedUserId,
            state,
            Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectedUserId = connectedUsersService.GetUserId(Context.ConnectionId);
        connectedUsersService.RemoveUser(Context.ConnectionId);

        if(exception != null)
            logger.LogError(exception,"User with id :{ConnectedUserId} with Connection ID: {SignalRConnectionId} had an error that leaded to disconnection.",
                connectedUserId,
                Context.ConnectionId);
        
        LogState(connectedUserId, "DisConnected");
        
        await base.OnDisconnectedAsync(exception);
    }
}