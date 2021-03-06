﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Routes
{
  public static class UserRoutes
  {
    public const string UserResource = "user";

    public const string GetUsersRoute = UserRoutes.UserResource;
    public const string GetCurrentUserRoute = UserRoutes.UserResource + "/me";
    public const string GetUserRoute = UserRoutes.UserResource + "/" + RouteParameterNames.IdParameterName;
    public const string PostUserRoute = UserRoutes.UserResource;
    public const string PutUserRoute = UserRoutes.UserResource + "/" + RouteParameterNames.IdParameterName;
    public const string DeleteUserRoute = UserRoutes.UserResource + "/" + RouteParameterNames.IdParameterName;
  }
}
