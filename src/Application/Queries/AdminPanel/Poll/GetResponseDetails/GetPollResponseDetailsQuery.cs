using Application.Queries.AdminPanel.Shops.CustomerCredits.CustomerCreditsInfo;
using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminPanel.Poll.GetResponseDetails;

public sealed record GetPollResponseDetailsQuery : IRequest<GetPollResponseDetailsResult>
{
    internal int Id { get; set; }
    public void SetId(int id) =>  Id = id;
}

public sealed record GetPollResponseDetailsResult(OrderInfoDto OrderInfo, CustomerInfoDto CustomerInfo, byte StarRate, string Note,List<FoodRatingDto> FoodRatings, List<QuestionsAnswerDto> QuestionsAnswered );

public sealed record CustomerInfoDto(string Name, string PhoneNumber);

public sealed record FoodRatingDto(string MenuItemName, string Picture, byte Rating);
public sealed record QuestionsAnswerDto(string QuestionTitle, QuestionType Type, string Answer);



    


