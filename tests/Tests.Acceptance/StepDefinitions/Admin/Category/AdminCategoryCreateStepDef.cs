using Application.Commands.AdminPanel.Categories.Create;
using FluentAssertions;
using Infra.Database;
using Reqnroll;
using Tests.Shared;

namespace Tests.Acceptance.StepDefinitions.Admin.Category;
[Binding]
public class AdminCategoryCreateStepDef(TestServerFixture fixture, SharedContext sharedContext) : IntegrationTestBase<CreateCategoryCommand, CreateCategoryResult>(fixture,sharedContext)
{
    protected override string RequestUri => "api/admin-panel/category/";
    
    [Given("A category with name {string} and image name {string} exists")]
    public void GivenACategoryWithNameAndImageNameExists(string name, string imageName)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var newCategory = Generator.Generate<Domain.Entities.Menu.Category>();
            newCategory.Name = name;
            newCategory.ImageName = imageName;
            
            context.Add(newCategory);
        });
    }

    [When("Admin provides name: {string} and image name: {string}")]
    public void WhenAdminProvidesNameAndImageName(string name, string imageName)
    {
        Request = new CreateCategoryCommand
        {
            Name = name,
            ImageName = imageName
        };
    }

    [When("Admin sends the create category request")]
    public async Task WhenAdminSendsTheCreateCategoryRequest()
    {
        Login();
        await Post();
    }

    [Then("A category with name {string} and image name: {string} should exist in the database")]
    public void ThenACategoryWithNameAndImageNameShouldExistInTheDatabase(string name, string imageName)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var category = context.Categories.First(x => x.Name == name && x.ImageName == imageName);

            category.Should().NotBeNull();
        });
    }
}