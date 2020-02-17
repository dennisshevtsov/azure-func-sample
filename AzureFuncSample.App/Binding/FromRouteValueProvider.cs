// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Newtonsoft.Json.Linq;

  public sealed class FromRouteValueProvider : IValueProvider
  {
    private readonly HttpRequest _httpRequest;
    private readonly object _value;

    public Type Type { get; }

    public FromRouteValueProvider(HttpRequest httpRequest, Type type)
    {
      _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
      Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public FromRouteValueProvider(object value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Task<object> GetValueAsync()
    {
      if (_value != null)
      {
        return Task.FromResult(_value);
      }

      var json = new JObject();

      foreach (var routeValue in _httpRequest.HttpContext.GetRouteData().Values)
      {
        json.Add(routeValue.Key, routeValue.Value.ToString());
      }

      var instance = json.ToObject(Type);

      return Task.FromResult(instance);
    }

    public string ToInvokeString() => Type.ToString();
  }
}
