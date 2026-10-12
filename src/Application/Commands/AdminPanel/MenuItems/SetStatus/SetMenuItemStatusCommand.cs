using MediatR;

namespace Application.Commands.AdminPanel.MenuItems.SetStatus;

public class SetMenuItemStatusCommand : IRequest
{
    public int MenuItemId { get; set; }
}