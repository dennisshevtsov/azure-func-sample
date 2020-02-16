namespace AzureFuncSample.App.Routes
{
  public static class UserRoutes
  {
    public const string UserResource = "user";

    public const string GetUsersRoute = UserRoutes.UserResource;
    public const string GetUserRoute = UserRoutes.UserResource + "/" + RouteParameterNames.IdParameterName;
    public const string PostUserRoute = UserRoutes.UserResource;
    public const string PutUserRoute = UserRoutes.UserResource + "/" + RouteParameterNames.IdParameterName;
    public const string DeleteUserRoute = UserRoutes.UserResource + "/" + RouteParameterNames.IdParameterName;
  }
}
