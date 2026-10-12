using MediatR;

namespace Application.Queries.Customer.GetProfile;

public sealed record CustomerGetProfileResult(string Name);
public sealed record CustomerGetProfileCommand : IRequest<CustomerGetProfileResult>;