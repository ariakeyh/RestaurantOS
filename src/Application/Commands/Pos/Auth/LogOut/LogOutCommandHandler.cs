using Application.Interface;
using MediatR;

namespace Application.Commands.Pos.Auth.LogOut;

public class LogOutCommandHandler(IAppDbContext dbContext, IAuthService authService) : IRequestHandler<LogOutCommand>
{
    public async Task Handle(LogOutCommand request, CancellationToken cancellationToken)
    {
        await authService.Logout();
    }
}