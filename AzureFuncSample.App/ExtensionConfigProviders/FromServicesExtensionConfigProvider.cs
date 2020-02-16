using AzureFuncSample.App.Attributes;
using AzureFuncSample.Runtime.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFuncSample.App.ExtensionConfigProviders
{
  public sealed class FromServicesExtensionConfigProvider : IExtensionConfigProvider
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FromServicesExtensionConfigProvider(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public void Initialize(ExtensionConfigContext context)
    {
      context.AddBindingRule<FromServicesAttribute>()
             .BindToInput(attribute => new UserService());
    }

    private TService FromServices<TService>(FromServicesAttribute attribute)
      => _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<TService>();
  }
}
