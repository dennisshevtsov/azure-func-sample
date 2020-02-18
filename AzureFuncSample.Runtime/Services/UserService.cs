// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.Runtime.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFuncSample.Runtime.Entities;
  using AzureFuncSample.Runtime.Queries;

  public sealed class UserService : IUserService
  {
    public Task<ExecutionResult<IEnumerable<UserEntity>>> GetUsersAsync(
      GetUsersQuery query, CancellationToken cancellationToken)
      => Task.FromResult(ExecutionResult<IEnumerable<UserEntity>>.Success(GetUsers(10)));

    public Task<ExecutionResult<UserEntity>> GetUserAsync(
      GetUserQuery query, CancellationToken cancellationToken)
      => Task.FromResult(ExecutionResult<UserEntity>.Success(GetUser(query.Id)));

    public Task<ExecutionResult<UserEntity>> CreateUserAsync(
      UserEntity userEntity, CancellationToken cancellationToken)
      => Task.FromResult(ExecutionResult<UserEntity>.Success(userEntity));

    public Task<ExecutionResult<UserEntity>> AuthorizeAsync(CancellationToken cancellationToken)
      //=> Task.FromResult(ExecutionResult<UserEntity>.Success(GetUser(Guid.NewGuid())));
      => Task.FromResult(ExecutionResult<UserEntity>.Fail("Unauthorized."));

    private static IEnumerable<UserEntity> GetUsers(int userCount)
    {
      for (var i = 0; i < userCount; ++i)
      {
        yield return GetUser(Guid.NewGuid());
      }
    }

    private static UserEntity GetUser(Guid id) => new UserEntity
                                                  {
                                                    Id = id,
                                                    Name = "test",
                                                  };
  }
}
