// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  public sealed class FromServicesBinding : IBinding
  {
    private readonly Type _serviceType;

    public FromServicesBinding(Type serviceType)
      => _serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));

    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
    {
      if (value != null && value.GetType() == _serviceType)
      {
        return Task.FromResult<IValueProvider>(new FromServicesValueProvider(value));
      }

      throw new InvalidOperationException();
    }

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(new FromServicesValueProvider(httpRequest, _serviceType));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor { Name = "service", };
  }
}
