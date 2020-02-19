// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  
  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Newtonsoft.Json.Linq;

  public sealed class FromQueryValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;
    private readonly object _value;

    public FromQueryValueProvider(HttpRequest httpRequest, Type type)
    {
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
      Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public FromQueryValueProvider(object value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
      Type = value.GetType();
    }

    public Type Type { get; }

    public Task<object> GetValueAsync()
    {
      if (_value != null)
      {
        return Task.FromResult(_value);
      }

      var json = new JObject();

      foreach (var queryParam in _httpRequest.Query)
      {
        json.Add(queryParam.Key, queryParam.Value.First());
      }

      var instance = json.ToObject(Type);

      return Task.FromResult(instance);
    }

    public string ToInvokeString() => Type.ToString();
  }
}
