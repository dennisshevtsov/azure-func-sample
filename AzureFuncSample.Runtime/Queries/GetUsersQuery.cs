// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.Runtime.Queries
{
  public sealed class GetUsersQuery
  {
    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public string Search { get; set; }
  }
}
