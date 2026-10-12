namespace Application.Interface;

public interface IOtpService
{
    Task<bool> SendOtp(string phoneNumber, string code);
}