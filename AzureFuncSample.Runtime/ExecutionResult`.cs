// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.Runtime
{
  using System;

  public sealed class ExecutionResult<T> : ExecutionResult, IExecutionResult<T> where T : class
  {
    internal ExecutionResult(T result) => Result = result ?? throw new ArgumentNullException(nameof(result));

    internal ExecutionResult(string error) : base(error) { }

    public T Result { get; }
  }
}
