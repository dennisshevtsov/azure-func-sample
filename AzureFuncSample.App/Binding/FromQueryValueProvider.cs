// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Newtonsoft.Json.Linq;

  public sealed class FromQueryValueProvider : IValueProvider
  {
    private readonly IReadOnlyDictionary<string, string> _queryParams;
    private readonly object _value;

    public FromQueryValueProvider(IReadOnlyDictionary<string, string> queryParams, Type type)
    {
      _queryParams = queryParams ?? throw new ArgumentNullException(nameof(queryParams));
      Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public FromQueryValueProvider(object value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Type Type { get; }

    public Task<object> GetValueAsync()
    {
      var json = new JObject();

      foreach (var queryParam in _queryParams)
      {
        json.Add(queryParam.Key, queryParam.Value);
      }

      var instance = json.ToObject(Type);

      return Task.FromResult(instance);
    }

    public string ToInvokeString() => Type.ToString();
  }
}
