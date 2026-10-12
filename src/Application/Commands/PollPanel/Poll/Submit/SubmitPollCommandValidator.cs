using FluentValidation;

namespace Application.Commands.PollPanel.Poll.Submit;

public class SubmitPollCommandValidator : AbstractValidator<SubmitPollCommand>
{
    public SubmitPollCommandValidator()
    {
        RuleFor(x => x.Rating).GreaterThan((byte)0).LessThan((byte)6);
        RuleForEach(x => x.FoodRatings).ChildRules(fr =>
        {
            fr.RuleFor(x => x.Rating).GreaterThan((byte)0).LessThan((byte)6);
        });
    }
}