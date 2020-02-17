// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Extensions
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Mvc;

  using AzureFuncSample.Runtime;

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
