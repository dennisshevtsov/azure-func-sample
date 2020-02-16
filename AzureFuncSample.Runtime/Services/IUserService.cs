using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AzureFuncSample.Runtime.Entities;
using AzureFuncSample.Runtime.Queries;

namespace AzureFuncSample.Runtime.Services
{
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
