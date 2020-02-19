// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Threading.Tasks;
  
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  using AzureFuncSample.Runtime.Entities;

  public sealed class AuthorizeBinding : IBinding
  {
    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
    {
      if (value != null && value is UserEntity userEntity)
      {
        return Task.FromResult<IValueProvider>(new AuthorizeValueProvider(userEntity));
      }

      throw new InvalidOperationException("A type of a value that a binding binds with a value provider should be UserEntity.");
    }

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.TryGetHttpRequest(out var httpRequest))
      {
        return Task.FromResult<IValueProvider>(new AuthorizeValueProvider(httpRequest));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor { Name = "authorize", };
  }
}
