using MediatR;

namespace Application.Commands.AdminPanel.MenuItems.Delete;

public record DeleteMenuItemCommand(int Id) : IRequest;