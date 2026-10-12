using Application.Commands.AdminPanel.Categories.Edit;
using FluentAssertions;
using Infra.Database;
using Microsoft.EntityFrameworkCore;
using Reqnroll;
using Tests.Shared;

namespace Tests.Acceptance.StepDefinitions.Admin.Category;
[Binding]
public class AdminCategoryEditStepDef(TestServerFixture fixture,SharedContext sharedContext) : IntegrationTestBase<EditCategoryCommand,EditCategoryResult>(fixture,sharedContext)
{
    private int CategoryId { get; set; }
    protected override string RequestUri => "api/admin-panel/category/" + CategoryId;
    
    [Given("Another category with name: {string} and image name {string} and Id {string} exists")]
    public void GivenAnotherCategoryWithNameAndImageNameAndIdExists(string name, string imageName, int id)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var newCategory = Generator.Generate<Domain.Entities.Menu.Category>();

            newCategory.Id = id;
            newCategory.Name = name;
            newCategory.ImageName = imageName;

            context.Add(newCategory);
            context.SaveChanges();
        });
    }

    [Given("Admin provides name: {string} and image name {string} and Id {string} and sends the request")]
    public async Task GivenAdminProvidesNameAndImageNameAndIdAndSendsTheRequest(string name, string imageName, int id)
    {
        Request = new EditCategoryCommand
        {
            Name = name,
            ImageName = imageName,
        };
        CategoryId = id;
        Login();
        await Put();
    }

    [Given("A category with name {string} and image name {string} and Id {string} exists")]
    public void GivenACategoryWithNameAndImageNameAndIdExists(string test, string imageName, int id)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var newCategory = Generator.Generate<Domain.Entities.Menu.Category>();
            newCategory.Name = test;
            newCategory.ImageName = imageName;
            newCategory.Id = id;
            
            context.Add(newCategory);
            context.SaveChanges();
        });
    }
    
    [Then("the category's name should have changed to {string}")]
    public void ThenTheCategorysNameShouldHaveChangedTo(string name)
    {
        DbContextAction<AppDbContext>(context =>
        {
            var category = context.Categories.FirstAsync(x => x.Name == name);

            category.Should().NotBeNull();
        });
    }
}