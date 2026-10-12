using System.Net;
using System.Text;
using Api.Middlewares;
using Application.Interface;
using Bogus;
using Domain.Entities.Auth;
using Domain.Entities.Shops;
using Domain.Enums;
using Infra.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Tests.Shared;
using Xunit;

namespace Tests.Acceptance;

//[Collection("MyCollection")]
public abstract class IntegrationTestBase<TReq, TRes> : IClassFixture<TestServerFixture> where TReq: IRequest<TRes> where TRes: class
{
    private HttpClient _client;
    private readonly TestServerFixture _fixture;
    private readonly SharedContext _context;
    protected TReq Request { get; set; }
    protected HttpResponse<TRes> Response { get; set; }

    protected IntegrationTestBase(TestServerFixture fix, SharedContext context)
    {
        _fixture = fix;
        _client = _fixture.CreateClient();
        _context = context;
        CleanDataBase();
    }

    protected abstract string RequestUri { get; }

    protected static Faker Faker => new();

    protected T DbContextAction<T, TContext>(Func<TContext, T> action) where TContext : DbContext
    {
        using var scope = _fixture.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<TContext>();

        return action(db);
    }

    protected void Login()
    {
        using var scope = _fixture.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var authService = scopedServices.GetRequiredService<IAuthService>();
        var userToLogin = new ShopUser();  
        DbContextAction<AppDbContext>(context =>
        {
            var testUser = Generator.Generate<ShopUser>();
            context.ShopUsers.Add(testUser);
            context.SaveChanges();
        });
        var token = authService.Login(userToLogin);
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    // protected void Login(string userName = "admin", bool isMainAdmin = false, params Permissions[] permissions)
    // {
    //     var userId = 0;
    //     var newOperator = new Operator
    //     {
    //         Username = userName,
    //         Password = PasswordEncryptor.EncryptPassword(Faker.Person.FirstName),
    //         PicturePath = "/",
    //         IsMainAdmin = isMainAdmin,
    //         Permissions = permissions.Select(x => new OperatorPermission
    //         {
    //             Permission = new Permission
    //             {
    //                 Title = x.ToString(),
    //                 Code = x.ToString()
    //             }
    //         }).ToList()
    //     };
    //
    //     DbContextAction<AppDbContext>(context =>
    //     {
    //         context.Operators.Add(newOperator);
    //         context.SaveChanges();
    //         userId = newOperator.Id;
    //     });
    //
    //     Login(userId, userName);
    // }

    public void Logout()
    {
        _client?.DefaultRequestHeaders.Remove("Authorization");
    }


    protected void DbContextAction<TContext>(Action<TContext> action) where TContext : DbContext
    {
        using var scope = _fixture.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<TContext>();
        action(db);
        db.SaveChanges();
    }

    protected async Task Post()
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
        var httpResponse = await _client.PostAsync(RequestUri, jsonContent, CancellationToken.None);

        Response = await GenerateResponse<TRes>(httpResponse);
    }

    protected async Task Put()
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

        var httpResponse = await _client.PutAsync(RequestUri, jsonContent, CancellationToken.None);

        Response = await GenerateResponse<TRes>(httpResponse);
    }

    protected async Task Delete()
    {
        var httpResponse = await _client.DeleteAsync(RequestUri, CancellationToken.None);

        Response = await GenerateResponse<TRes>(httpResponse);
    }

    protected async Task Get()
    {
        var httpResponse = await _client.GetAsync(RequestUri, CancellationToken.None);

        Response = await GenerateResponse<TRes>(httpResponse);
    }

    protected async Task GetWithPagination(string routeParams)
    
    {
        var httpResponse =
            await _client.GetAsync(RequestUri + routeParams,
                CancellationToken.None);

        Response = await GenerateResponse<TRes>(httpResponse);
    }


    private async Task<HttpResponse<T>> GenerateResponse<T>(HttpResponseMessage httpResponse) where T : class
    
    {
        var response = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorsList = JsonConvert.DeserializeObject<ApiResponseWithValidationErrors>(response);
            _context.LastHttpStatusCode = (int)httpResponse.StatusCode;
            _context.ResponseMessage = string.Empty;
            return new HttpResponse<T>(null, httpResponse.StatusCode, string.Empty, errorsList.Data.Errors);
        }

        if (response == string.Empty && httpResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            _context.LastHttpStatusCode = (int)httpResponse.StatusCode;
            _context.ResponseMessage = string.Empty;
            return new HttpResponse<T>(null, httpResponse.StatusCode, string.Empty,
                new Dictionary<string, List<string>>());
        }

        var deserializedContent = JsonConvert.DeserializeObject<ApiResponse<T>>(response);
        if (deserializedContent is null)
            throw new Exception($"Error while requesting.");

        _context.LastHttpStatusCode = (int)httpResponse.StatusCode;
        _context.ResponseMessage = deserializedContent.Message;
        return new HttpResponse<T>(deserializedContent.Data, httpResponse.StatusCode, deserializedContent.Message,
            new Dictionary<string, List<string>>());
    }

    public Task InitializeAsync()
    {
        _client = _fixture.CreateClient();

        CleanDataBase();

        return Task.CompletedTask;
    }

    private static void CleanFileDirectory()
    {
        var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        if (!Directory.Exists(fileDirectory)) return;
        var files = Directory.GetFiles(fileDirectory);

        foreach (var file in files)
        {
            // Delete each file
            File.Delete(file);
        }
    }

    private void CleanDataBase()
    {
        DbContextAction<AppDbContext>(x =>
        {
            x.Database.EnsureDeleted();
        });
    }

    public Task DisposeAsync()
    {
        try
        {
            CleanDataBase();
            CleanFileDirectory();

          //  _client?.SafeDispose();
           // _client = null;
        }
        catch
        {
            Console.WriteLine($"Cleanup failed: ");
        }

        return Task.CompletedTask;
    }

    public record HttpResponse<T>(
        T Result,
        HttpStatusCode StatusCode,
        string Message,
        Dictionary<string, List<string>> ValidationErrors) where T : class;
}