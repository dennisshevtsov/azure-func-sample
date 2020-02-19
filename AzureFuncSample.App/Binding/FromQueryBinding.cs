// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  public sealed class FromQueryBinding : IBinding
  {
    private readonly Type _queryParamsObjectType;

    public FromQueryBinding(Type queryParamsObjectType)
        => _queryParamsObjectType = queryParamsObjectType ?? throw new ArgumentNullException(nameof(queryParamsObjectType));

    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
    {
      if (value != null && value.GetType() == _queryParamsObjectType)
      {
        return Task.FromResult<IValueProvider>(new FromQueryValueProvider(value));
      }

      throw new InvalidOperationException();
    }

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(new FromQueryValueProvider(httpRequest, _queryParamsObjectType));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor { Name = "queryParams", };
  }
}
