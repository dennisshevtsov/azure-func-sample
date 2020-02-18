// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information

namespace AzureFuncSample.App.Binding
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs.Host.Bindings;
  using Microsoft.Extensions.DependencyInjection;

  using AzureFuncSample.Runtime.Entities;
  using AzureFuncSample.Runtime.Services;

  public sealed class AuthorizeValueProvider : IValueProvider
  {
    private readonly UserEntity _userEntity;
    private readonly HttpRequest _httpRequest;

    public AuthorizeValueProvider(UserEntity userEntity)
      => _userEntity = userEntity ?? throw new ArgumentNullException(nameof(userEntity));

    public AuthorizeValueProvider(HttpRequest httpRequest)
      => _httpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));

    public Type Type => typeof(UserEntity);

    public async Task<object> GetValueAsync()
    {
      var userService = _httpRequest.HttpContext.RequestServices.GetRequiredService<IUserService>();
      var authorizeResult = await userService.AuthorizeAsync(_httpRequest.HttpContext.RequestAborted);

      if (authorizeResult.HasError)
      {
        _httpRequest.HttpContext.Response.ContentType = "application/json";
        _httpRequest.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await _httpRequest.HttpContext.Response.WriteAsync("{ \"message\": \"Unauthorized.\" }");

        throw new Exception();
      }

      return authorizeResult.Result;
    }

    public string ToInvokeString() => "authorize";
  }
}
