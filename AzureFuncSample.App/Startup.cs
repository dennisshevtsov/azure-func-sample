using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using AzureFuncSample.Runtime.Services;

[assembly: FunctionsStartup(typeof(AzureFuncSample.App.Startup))]

namespace AzureFuncSample.App
{
  public sealed class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddScoped<IUserService, UserService>();
    }
  }
}
