// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.Runtime.Services
{
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFuncSample.Runtime.Entities;
  using AzureFuncSample.Runtime.Queries;

  public interface IUserService
  {
    public Task<ExecutionResult<IEnumerable<UserEntity>>> GetUsersAsync(
      GetUsersQuery query, CancellationToken cancellationToken);

    public Task<ExecutionResult<UserEntity>> GetUserAsync(
      GetUserQuery query, CancellationToken cancellationToken);

    public Task<ExecutionResult<UserEntity>> CreateUserAsync(
      UserEntity userEntity, CancellationToken cancellationToken);
  }
}
