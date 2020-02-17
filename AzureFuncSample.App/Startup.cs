// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(AzureFuncSample.App.Startup))]

namespace AzureFuncSample.App
{
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  using AzureFuncSample.App.Binding;
  using AzureFuncSample.Runtime.Services;

  public sealed class Startup : IWebJobsStartup
  {
    public void Configure(IWebJobsBuilder builder)
    {
      builder.AddExtension<FromBodyExtensionConfigProvider>();
      builder.AddExtension<FromRouteExtensionConfigProvider>();
      builder.AddExtension<FromQueryExtensionConfigProvider>();
      builder.AddExtension<FromServicesExtensionConfigProvider>();

      builder.Services.AddScoped<IUserService, UserService>();
    }
  }
}
