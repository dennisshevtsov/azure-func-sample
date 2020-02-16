using System;

namespace AzureFuncSample.Runtime
{
  public class ExecutionResult
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

    public bool HasError => !string.IsNullOrWhiteSpace(Error);

    public static ExecutionResult Success() => new ExecutionResult();

    public static ExecutionResult Fail(string error) => new ExecutionResult(error);
  }

  public sealed class ExecutionResult<T> : ExecutionResult where T : class
  {
    public ExecutionResult(T result) => Result = result ?? throw new ArgumentNullException(nameof(result));

    public ExecutionResult(string error) : base(error) { }

    public T Result { get; }

    public static ExecutionResult<T> Success(T result) => new ExecutionResult<T>(result);

    public static new ExecutionResult<T> Fail(string error) => new ExecutionResult<T>(error);
  }
}
