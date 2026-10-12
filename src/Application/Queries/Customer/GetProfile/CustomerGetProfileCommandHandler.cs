using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Customer.GetProfile;

public class CustomerGetProfileCommandHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<CustomerGetProfileCommand, CustomerGetProfileResult>
{
    public async Task<CustomerGetProfileResult> Handle(CustomerGetProfileCommand request, CancellationToken cancellationToken)
    {
        var currentCustomerId = authService.GetUserId();
        
        var customerName = await context.Users.Where(x=>x.Id == currentCustomerId).Select(x=>x.Name).FirstOrDefaultAsync(cancellationToken);
        
        return new CustomerGetProfileResult(customerName!);
    }
}