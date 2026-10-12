using Api.Bootstraps;
using Infra;

var builder = WebApplication.CreateBuilder(args);
await builder.Services.AddInfraData(builder.Environment.EnvironmentName);

var app = builder.BootstrapServices(builder.Configuration);
await app.WrapUp();
await app.RunAsync();

namespace Api
{
    public partial class Program
    {
        protected Program()
        {
        }
    }
}