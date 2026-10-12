namespace Application.Interface;

public interface ICaptchaService
{
    Task<bool> VerifyAsync(string responseToken, CancellationToken cancellationToken = default);
}