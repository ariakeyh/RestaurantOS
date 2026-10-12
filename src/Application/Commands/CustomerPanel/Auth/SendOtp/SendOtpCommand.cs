using MediatR;

namespace Application.Commands.Customer.Auth.SendOtp;

public sealed record SendOtpResult(string Expiration);
public sealed record SendOtpCommand(string PhoneNumber) : IRequest<SendOtpResult>;

    
