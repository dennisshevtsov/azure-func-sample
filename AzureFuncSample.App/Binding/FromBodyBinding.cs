// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Threading.Tasks;
  
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Azure.WebJobs.Host.Protocols;

  public sealed class FromBodyBinding : IBinding
  {
    private readonly Type _bodyObjectType;

    public FromBodyBinding(Type bodyObjectType)
      => _bodyObjectType = bodyObjectType ?? throw new ArgumentNullException(nameof(bodyObjectType));

    public bool FromAttribute => true;

    public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
      => Task.FromResult<IValueProvider>(new FromBodyValueProvider(value));

    public Task<IValueProvider> BindAsync(BindingContext context)
    {
      if (context.BindingData["$request"] is HttpRequest httpRequest)
      {
        return Task.FromResult<IValueProvider>(new FromBodyValueProvider(httpRequest, _bodyObjectType));
      }

      throw new InvalidOperationException();
    }

    public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor { Name = "body", };
  }
}
