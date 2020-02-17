// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  public sealed class FromServicesValueProvider : IValueProvider
  {
    private readonly IServiceProvider _serviceProvider;
    private object _value;

    public FromServicesValueProvider(IServiceProvider serviceProvider, Type serviceType)
    {
      _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      Type = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
    }

    public FromServicesValueProvider(object value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
      Type = _value.GetType();
    }

    public Type Type { get; }

    public Task<object> GetValueAsync() => Task.FromResult(_serviceProvider.GetRequiredService(Type));

    public string ToInvokeString() => _value?.ToString() ?? string.Empty;
  }
}
