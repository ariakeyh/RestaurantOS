using MediatR;

namespace Application.Queries.AdminPanel.Customer.GetName;

public sealed record GetCustomerNameQuery(string PhoneNumber) : IRequest<string?>;