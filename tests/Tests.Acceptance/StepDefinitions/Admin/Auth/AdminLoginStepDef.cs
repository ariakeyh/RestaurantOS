using Application.Commands.AdminPanel.Auth;
using Common.Utils;
using Domain.Entities.Auth;
using FluentAssertions;
using Infra.Database;
using Reqnroll;
using Tests.Shared;

namespace Tests.Acceptance.StepDefinitions.Admin.Auth;

[Binding]
public class AdminLoginStepDef(TestServerFixture fixture, SharedContext sharedContext) : IntegrationTestBase<AdminLoginRequest, AdminLoginResult>(fixture, sharedContext)
{
    protected override string RequestUri => "api/admin-panel/auth";

    [When("admin sends a login request")]
    public async Task WhenAdminSendsALoginRequest()
    {
        await Post();
    }
    
    [Then("jwt token should not be empty")]
    public void ThenJwtTokenShouldNotBeEmpty()
    {
        Response.Result.Token.Should().NotBeEmpty();
    }

    [When("they provide credentials phone number: {string} and password: {string}")]
    public void WhenTheyProvideCredentialsPhoneNumberAndPassword(string phoneNumber, string password)
    {
        Request =  new AdminLoginRequest(phoneNumber, password);
    }

    [Given("a user with phone number: {string} and password: {string} exists")]
    public void GivenAUserWithPhoneNumberAndPasswordExists(string phoneNumber, string password)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var newUser = Generator.Generate<User>();

            newUser.Name = Faker.Name.FirstName();
            newUser.PhoneNumber = phoneNumber;
            newUser.AdminPasswordHashed = PasswordEncryptor.EncryptPassword(password);
            context.Users.Add(newUser);
            context.SaveChanges();
        });
    }
}