using Application.Commands.Customer.Profile;
using Application.Interface;
using Common;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CustomerPanel.Profile;

public class CustomerEditProfileCommandHandler(IAppDbContext context, IAuthService authService) : IRequestHandler<CustomerEditProfileCommand, CustomerEditProfileResult>
{
    public async Task<CustomerEditProfileResult> Handle(CustomerEditProfileCommand request, CancellationToken cancellationToken)
    {
        var currentCustomer = await context.Users.FirstOrDefaultAsync(x=>x.Id == request.Id,cancellationToken);

        if (currentCustomer is null)
            throw new NotFoundException(ErrorMessages.NotFound);
        
        currentCustomer.Name = request.NewName;
        
        await context.SaveChangesAsync(cancellationToken);
        return new CustomerEditProfileResult(currentCustomer.Id, currentCustomer.Name);
    }
}