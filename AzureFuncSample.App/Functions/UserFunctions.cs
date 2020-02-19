// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Functions
{
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Extensions.Http;

  using AzureFuncSample.App.Attributes;
  using AzureFuncSample.App.Extensions;
  using AzureFuncSample.App.Http;
  using AzureFuncSample.App.Routes;
  using AzureFuncSample.Runtime;
  using AzureFuncSample.Runtime.Entities;
  using AzureFuncSample.Runtime.Queries;
  using AzureFuncSample.Runtime.Services;

  public static class UserFunctions
  {
    [FunctionName(nameof(GetCurrentUserFunction))]
    public static UserEntity GetCurrentUserFunction(
      [HttpTrigger(AuthorizationLevel.Admin,
                   HttpMethodTypes.Get,
                   Route = UserRoutes.GetCurrentUserRoute)] HttpRequest httpRequest,
      [Authorize] UserEntity authorizedUserEntity)
      => authorizedUserEntity;

    [FunctionName(nameof(GetUsersFunction))]
    public static async Task<IExecutionResult> GetUsersFunction(
      [HttpTrigger(AuthorizationLevel.Admin,
                   HttpMethodTypes.Get,
                   Route = UserRoutes.GetUsersRoute)] HttpRequest httpRequest,
      [FromServices] IUserService userService,
      [FromQuery] GetUsersQuery query,
      CancellationToken cancellationToken)
      => await userService.GetUsersAsync(query, cancellationToken);

    [FunctionName(nameof(GetUserFunction))]
    public static async Task<IExecutionResult> GetUserFunction(
      [HttpTrigger(AuthorizationLevel.Admin,
                   HttpMethodTypes.Get,
                   Route = UserRoutes.GetUserRoute)] HttpRequest httpRequest,
      [FromServices] IUserService userService,
      [FromRoute] GetUserQuery query,
      CancellationToken cancellationToken)
      => await userService.GetUserAsync(query, cancellationToken);

    [FunctionName(nameof(PostUserFunction))]
    public static async Task<IExecutionResult> PostUserFunction(
      [HttpTrigger(AuthorizationLevel.Admin,
                   HttpMethodTypes.Post,
                   Route = UserRoutes.PostUserRoute)] HttpRequest httpRequest,
      [FromServices] IUserService userService,
      [FromBody] UserEntity userEntity,
      CancellationToken cancellationToken)
      => await userService.CreateUserAsync(userEntity, cancellationToken);
  }
}
