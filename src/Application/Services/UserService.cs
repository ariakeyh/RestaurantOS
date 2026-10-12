using Application.Interface;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService(IAppDbContext context)
{
    public async Task<User> AddUser(string name, string phoneNumber, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(x=> x.PhoneNumber == phoneNumber)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user != null)
            return user;

        user = new User
        {
            PhoneNumber = phoneNumber,
            Name = name
        };
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }
}