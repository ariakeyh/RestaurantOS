using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interface;
using Domain.Entities.Auth;
using Domain.Entities.Shops;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers;

public class AuthService(IHttpContextAccessor context, IConfiguration configuration) : IAuthService
{
    private const string ShopIdClaim = "ShopId";
    private const string ShopUserIdClaim = "ShopUserId";
    private const string UserTypeClaim = "UserType";

    public int GetShopId()
    {
        var user = context.HttpContext!.User;
        if (user.Identity!.IsAuthenticated)
            return int.Parse(user.Claims.First(x => x.Type == ShopIdClaim).Value);

        throw new UnauthorizedAccessException("Access Denied");
    }

    public Guid GetShopUserId()
    {
        var user = context.HttpContext!.User;
        if (user.Identity!.IsAuthenticated)
            return Guid.Parse(user.Claims.First(x => x.Type == ShopUserIdClaim).Value);
        throw new UnauthorizedAccessException("Access Denied");
    }

    public RequestInfo GetCurrentRequestInfo()
    {
        var agent = context.HttpContext?.Request.Headers.UserAgent.ToString();
        var ipAddress = context.HttpContext?.Connection.RemoteIpAddress?.ToString();
        var nameIdClaim = context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        var value = nameIdClaim?.Value;
        var userKey = context.HttpContext?.User.Identity?.IsAuthenticated == false
            ? 0
            : int.Parse(value);
        return new RequestInfo(ipAddress, agent, DateTime.Now, userKey);
    }

    public int GetUserId()
    {
        var user = context.HttpContext!.User;
        if (user.Identity!.IsAuthenticated)
            return int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        throw new UnauthorizedAccessException("Access Denied");
    }

    public string Login(ShopUser shopUser)
    {
        var token = GenerateJwtTokens(shopUser.UserId, shopUser.Id, shopUser.User.Name, shopUser.ShopId, shopUser.Type);
        return token;
    }

    public string Login(User user)
    {
        throw new NotImplementedException();
    }


    public Task Logout()
    {
        context.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Task.CompletedTask;
    }

    private string GenerateJwtTokens(int userId, Guid shopUserId, string name, int shopId, ShopUserType userType)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, name),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ShopUserIdClaim, shopUserId.ToString()),
            new(ShopIdClaim, shopId.ToString()),
            new(UserTypeClaim, userType.ToString())
        };

        var secretKey = configuration["JWT:SecretKey"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = userType switch
        {
            ShopUserType.POS => DateTime.UtcNow.AddMonths(6),
            _ => DateTime.UtcNow.AddHours(4) //TODO : Must come from config
        };

        var accessToken = new JwtSecurityToken(
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        context.HttpContext.User = new ClaimsPrincipal(identity);
        return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }
}