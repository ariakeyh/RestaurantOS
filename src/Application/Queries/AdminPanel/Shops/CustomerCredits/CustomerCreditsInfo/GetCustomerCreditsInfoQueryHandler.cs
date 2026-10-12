using Application.Interface;
using Domain.Entities.Customers;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsInfo;

public class GetCustomerCreditsInfoQueryHandler(IAppDbContext context, IAuthService authService)
    : IRequestHandler<GetCustomerCreditsInfoQuery, List<CustomerCreditDto>>
{
    public async Task<List<CustomerCreditDto>> Handle(GetCustomerCreditsInfoQuery request,
        CancellationToken cancellationToken)
    {
        var currentShopId = authService.GetShopId();

        var credits = await context.CustomerCredits
            .Where(x => x.Id == request.Id && x.Customer.ShopId == currentShopId)
            .Include(x=>x.SpendHistories)
            .ThenInclude(x=>x.Order)
            .Include(x=>((OrderCustomerCredit)x).Order)
            .Include(x=>((OrderCustomerCredit)x).CashbackSetting)
            .ToListAsync(cancellationToken);
        
        var customerCredits = credits
            .Select(x => x.Type == CustomerCreditType.Manual
                ? (CustomerCreditDto)new ManualCustomerCreditDto
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Expiration = x.Expiration,
                    Type = x.Type,
                    SpentAmount = x.SpentAmount,
                    SpendHistory = x.SpendHistories
                        .Select(sh => new SpendHistoryDto(
                            sh.SpendAmount,
                            sh.BalanceAmount,
                            new OrderInfoDto(sh.Order.Id, sh.Order.CreatedAt, sh.Order.TotalPrice)))
                        .ToList(),
                    Reason = ((ManualCustomerCredit)x).Reason
                }
                : new OrderCustomerCreditDto
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Expiration = x.Expiration,
                    Type = x.Type,
                    SpentAmount = x.SpentAmount,
                    SpendHistory = x.SpendHistories
                        .Select(sh => new SpendHistoryDto(
                            sh.SpendAmount,
                            sh.BalanceAmount,
                            new OrderInfoDto(sh.Order.Id, sh.Order.CreatedAt, sh.Order.TotalPrice)))
                        .ToList(),
                    OrderInfo = new OrderInfoDto(((OrderCustomerCredit)x).Order.Id,((OrderCustomerCredit)x).Order.CreatedAt, ((OrderCustomerCredit)x).Order.TotalPrice),
                    CashbackInfo = new CashbackInfoDto(((OrderCustomerCredit)x).CashbackSetting.CashbackPercentage, ((OrderCustomerCredit)x).CashbackSetting.MaxCashbackAmount, ((OrderCustomerCredit)x).CashbackSetting.CreatedAt)
                })
            .ToList();

        return customerCredits;

    }
}