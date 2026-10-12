using Domain.Entities.Auth;
using Domain.Entities.Shops;
using Domain.ValueObjects;

namespace Application.Interface;

public interface IAuthService
{
    Task Logout();
    int GetUserId();
    int GetShopId();
    Guid GetShopUserId();
    RequestInfo GetCurrentRequestInfo();
    string Login(ShopUser shopUser);
    string Login(User user);
}