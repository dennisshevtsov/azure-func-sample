// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.Runtime
{
  public interface IExecutionResult<out T> : IExecutionResult where T : class
  {
    public T Result { get; }
  }
}
