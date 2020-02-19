// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.Runtime
{
  using System;

  public class ExecutionResult : IExecutionResult
  {
    protected ExecutionResult() { }

    protected ExecutionResult(string error)
    {
      if (string.IsNullOrWhiteSpace(error))
      {
        throw new ArgumentException($"Argument {nameof(error)} cannot be empty.");
      }
    }

    public string Error { get; }

    public bool HasError => Error != null;

    public static ExecutionResult Success() => new ExecutionResult();

    public static ExecutionResult<T> Success<T>(T result) where T : class => new ExecutionResult<T>(result);

    public static ExecutionResult Fail(string error) => new ExecutionResult(error);

    public static ExecutionResult<T> Fail<T>(string error) where T : class => new ExecutionResult<T>(error);
  }
}
