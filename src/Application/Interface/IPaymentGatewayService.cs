namespace Application.Interface;

public interface IPaymentGatewayService
{
    Task<(string Authority, string URL)> CreatePaymentLink(
        int amount,
        string description,
        string userPhoneNumber,
        CancellationToken cancellationToken);

    Task<(bool IsSuccess, string Message, string Payload)> VerifyPayment(
        string authority,
        decimal amount,
        CancellationToken cancellationToken);
}
