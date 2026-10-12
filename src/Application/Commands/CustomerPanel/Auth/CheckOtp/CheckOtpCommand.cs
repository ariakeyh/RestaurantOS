using MediatR;

namespace Application.Commands.CustomerPanel.Auth.CheckOtp;

public sealed record CheckOtpResult(string Token);
public sealed record CheckOtpCommand(string PhoneNumber,string Code) : IRequest<CheckOtpResult>;

    
