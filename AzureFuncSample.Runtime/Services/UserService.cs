using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AzureFuncSample.Runtime.Entities;
using AzureFuncSample.Runtime.Queries;

namespace AzureFuncSample.Runtime.Services
{
  public sealed class UserService : IUserService
  {
    public Task<IEnumerable<UserEntity>> GetUsersAsync(GetUsersQuery query,
                                                       CancellationToken cancellationToken)
      => Task.FromResult(GetUsers(10));

    public Task<UserEntity> GetUserAsync(GetUserQuery query, CancellationToken cancellationToken)
      => Task.FromResult(GetUser(query.Id));

    public Task<UserEntity> CreateUserAsync(UserEntity userEntity, CancellationToken cancellationToken)
      => Task.FromResult(userEntity);

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
