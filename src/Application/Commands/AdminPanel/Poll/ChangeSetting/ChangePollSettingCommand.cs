using MediatR;

namespace Application.Commands.AdminPanel.Poll.ChangeSetting;

public sealed record ChangePollSettingCommand(bool ShouldRateFood, int NotifyAfterDays,int[] QuestionsIds) : IRequest<ChangePollSettingResult>;
public sealed record ChangePollSettingResult;