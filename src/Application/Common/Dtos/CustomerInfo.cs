using Domain.Entities.Customers;
using Domain.Enums;

namespace Application.Common.Dtos;

public class CustomerInfo(Customer customer)
{
    public Guid CustomerId { get; set; } = customer.Id;
    public string Name { get; set; } = customer.Name;
    public string PhoneNumber { get; set; } = customer.User.PhoneNumber;
    public CustomerBadge Badge { get; set; } = customer.Badge;
}