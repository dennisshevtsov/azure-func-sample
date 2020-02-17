// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;

  public sealed class FromBodyValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;
    private readonly object _value;

    public FromBodyValueProvider(HttpRequest httpRequest, Type type)
    {
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
      Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public FromBodyValueProvider(object value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Type Type { get; }

    public async Task<object> GetValueAsync()
    {
      var instance = await JsonSerializer.DeserializeAsync(_httpRequest.Body,
                                                           Type,
                                                           new JsonSerializerOptions
                                                           {
                                                             PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                                           });

      return instance;
    }

    public string ToInvokeString() => Type.ToString();
  }
}
