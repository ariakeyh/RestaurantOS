using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Commands.AdminPanel.Auth;

public sealed record AdminLoginResult(string Token);
public sealed record AdminLoginRequest([Required] string PhoneNumber,[Required] string Password) : IRequest<AdminLoginResult>;