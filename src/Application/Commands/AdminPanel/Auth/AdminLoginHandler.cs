using Application.Interface;
using Common;
using Common.Exceptions;
using Common.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.AdminPanel.Auth;

public class AdminLoginHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<AdminLoginRequest, AdminLoginResult>
{
    public async Task<AdminLoginResult> Handle(AdminLoginRequest request, CancellationToken cancellationToken)
    {
        var hashedPassword = PasswordEncryptor.EncryptPassword(request.Password);
        var userId = await context.Users.Where
                (x => x.PhoneNumber == request.PhoneNumber && x.AdminPasswordHashed == hashedPassword)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (userId is 0)
            throw new NotFoundException(ErrorMessages.NotFound);

        var shopUser = await context.ShopUsers
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
        
        if(shopUser is null)
            throw new NotFoundException(ErrorMessages.NotFound);
        
        var token = authService.Login(shopUser);

        return new AdminLoginResult(token);
    }
}