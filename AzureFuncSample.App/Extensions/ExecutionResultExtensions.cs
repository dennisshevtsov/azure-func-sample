using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AzureFuncSample.Runtime;

namespace AzureFuncSample.App.Extensions
{
  public static class ExecutionResultExtensions
  {
    public static IActionResult ToActionResult<T>(this ExecutionResult<T> executionResult)
      where T : class
    {
      if (executionResult == null)
      {
        throw new ArgumentNullException(nameof(executionResult));
      }

      if (executionResult.HasError)
      {
        return new BadRequestObjectResult(executionResult.Error);
      }

      return new OkObjectResult(executionResult.Result);
    }

    public static Task<IActionResult> ToActionResult<T>(this Task<ExecutionResult<T>> executionResultTask,
                                                        CancellationToken cancellationToken)
      where T : class
    {
      if (executionResultTask == null)
      {
        throw new ArgumentNullException(nameof(executionResultTask));
      }

      return executionResultTask.ContinueWith(executionResult => executionResult.Result.ToActionResult(),
                                              cancellationToken);
    }
  }
}
