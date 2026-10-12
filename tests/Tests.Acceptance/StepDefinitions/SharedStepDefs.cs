using FluentAssertions;
using Reqnroll;

namespace Tests.Acceptance.StepDefinitions;

[Binding]

public class SharedStepDefs(SharedContext sharedContext)
{
    [Then("the response status code should be {string}")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        sharedContext.LastHttpStatusCode.Should().Be(statusCode);
    }

    [Then("the error message should be {string}")]
    public void ThenTheErrorMessageShouldBe(string errorMessage)
    {
        sharedContext.ResponseMessage.Should().Be(errorMessage);
    }
}