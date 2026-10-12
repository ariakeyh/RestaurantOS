using System.Security.Cryptography;
using Application.Interface;
using MediatR;

namespace Application.Commands.AdminPanel.POS.CreateToken;

public class CreatePosOwnershipCommandHandler(ICacheService cache,IAuthService authService) : IRequestHandler<CreatePosTokenCommand, CreatePosTokenResult>
{
    public async Task<CreatePosTokenResult> Handle(CreatePosTokenCommand request, CancellationToken cancellationToken)
    {
        //TODO : ITokenService and Hashing the value
        var randomNumber = RandomNumberGenerator.GetBytes(32);
        var token = Convert.ToHexString(randomNumber);
        
        var key = $"pos:token:{token}";
        var shopId = authService.GetShopId();
        await cache.SetValue(key, shopId.ToString(), TimeSpan.FromMinutes(5));

        return new CreatePosTokenResult(token);
    }
}