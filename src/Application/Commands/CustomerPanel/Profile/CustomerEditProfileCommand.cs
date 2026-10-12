using MediatR;

namespace Application.Commands.Customer.Profile;

public sealed record CustomerEditProfileResult(int UserId, string NewName);

public class CustomerEditProfileCommand : IRequest<CustomerEditProfileResult>
{
    internal int Id { get; set; }
    public string NewName { get; set; }
    public void SetId(int id) =>  Id = id;
}

    
