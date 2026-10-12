using Application.Commands.AdminPanel.Categories.Delete;
using FluentAssertions;
using Infra.Database;
using Reqnroll;
using Tests.Shared;

namespace Tests.Acceptance.StepDefinitions.Admin.Category;
[Binding]
public class AdminCategoryDeleteStepDef(TestServerFixture serverFixture,SharedContext sharedContext) : IntegrationTestBase<DeleteCategoryCommand,DeleteCategoryResult>(serverFixture,sharedContext)
{
    protected override string RequestUri => "api/admin-panel/category/" + Request.Id;
    
    [Given("A category with Id of {int} exists")]
    public void GivenACategoryWithIdOfExists(int id)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var newCategory = Generator.Generate<Domain.Entities.Menu.Category>();

            newCategory.Id = id;
            newCategory.Name = Faker.Lorem.Word();
            newCategory.ImageName = Faker.Lorem.Word();
            context.Add(newCategory);
            context.SaveChanges();
        });
    }

    [When("Admin provides Id {int} and sends the request")]
    public async Task WhenAdminProvidesIdAndSendsTheRequest(int id)
    {
        Request = new DeleteCategoryCommand(id);
        Login();
        await Delete();
    }

    [Then("category with Id {int} should be deleted")]
    public void ThenCategoryWithIdShouldBeDeleted(int id)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);

            category.Should().BeNull();
        });
    }
}