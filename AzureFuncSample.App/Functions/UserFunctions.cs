using System.Threading;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

using AzureFuncSample.App.Http;
using AzureFuncSample.App.Routes;
using AzureFuncSample.Runtime.Entities;
using AzureFuncSample.Runtime.Queries;
using AzureFuncSample.Runtime.Services;

namespace AzureFuncSample.App.Functions
{
  public static class UserFunctions
  {
    [FunctionName(nameof(GetUsersFunction))]
    public static IActionResult GetUsersFunction(
      [HttpTrigger(AuthorizationLevel.Admin,
                   HttpMethodTypes.Get,
                   Route = UserRoutes.GetUsersRoute)] HttpRequest request,
      [FromServices] IUserService userService,
      [FromQuery] GetUsersQuery query,
      CancellationToken cancellationToken)
      => new OkObjectResult(userService.GetUsersAsync(query, cancellationToken));

    [FunctionName(nameof(GetUserFunction))]
    public static IActionResult GetUserFunction(
      [HttpTrigger(AuthorizationLevel.Admin,
                   HttpMethodTypes.Get,
                   Route = UserRoutes.GetUserRoute)] HttpRequest request,
      [FromServices] IUserService userService,
      [FromRoute] GetUserQuery query,
      CancellationToken cancellationToken)
      => new OkObjectResult(userService.GetUserAsync(query, cancellationToken));

    [FunctionName(nameof(PostUserFunction))]
    public static IActionResult PostUserFunction(
      [HttpTrigger(AuthorizationLevel.Admin,
                   HttpMethodTypes.Post,
                   Route = UserRoutes.PostUserRoute)] HttpRequest request,
      [FromServices] IUserService userService,
      [FromBody] UserEntity userEntity,
      CancellationToken cancellationToken)
      => new OkObjectResult(userService.CreateUserAsync(userEntity, cancellationToken));
  }
}
