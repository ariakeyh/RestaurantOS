namespace Application.Services.OrderServices;

public class CashierSourceInfoDto : SourceInfoDto
{
    public int CashierId { get; set; }
    public string CashierName { get; set; }
}