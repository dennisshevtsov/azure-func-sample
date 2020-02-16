namespace AzureFuncSample.Runtime.Queries
{
  public sealed class GetUsersQuery
  {
    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public string Search { get; set; }
  }
}
