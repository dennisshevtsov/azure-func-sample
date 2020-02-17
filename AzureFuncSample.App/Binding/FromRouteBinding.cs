// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  public sealed class FromRouteBinding : IBinding
  {
    private readonly Type _routeParamObjectType;

    public FromRouteBinding(Type routeParamObjectType)
    {
      _routeParamObjectType = routeParamObjectType ?? throw new ArgumentNullException(nameof(routeParamObjectType));
    }

    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
      => Task.FromResult<IValueProvider>(new FromRouteValueProvider(value));

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.BindingData["$request"] is HttpRequest httpRequest)
      {
        return Task.FromResult<IValueProvider>(new FromRouteValueProvider(httpRequest, _routeParamObjectType));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor { Name = "routeParams", };
  }
}
