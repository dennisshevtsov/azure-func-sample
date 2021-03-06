﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using Microsoft.Azure.WebJobs.Host.Config;

  using AzureFuncSample.App.Attributes;

  public sealed class FromRouteExtensionConfigProvider : IExtensionConfigProvider
  {
    public void Initialize(ExtensionConfigContext context)
    {
      context.AddBindingRule<FromRouteAttribute>()
             .Bind(new FromRouteBindingProvider());
    }
  }
}
