using MediatR;

namespace Application.Commands.AdminPanel.ReportSettings.UpdateSettings;

public sealed record UpdateReportSettingsCommand(int[] ReportIds) : IRequest;





    
