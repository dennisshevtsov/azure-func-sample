// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFuncSample.App.Http
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.WebJobs.Extensions.Http;
  using Microsoft.Extensions.Options;

  using AzureFuncSample.Runtime;

  public sealed class HttpPostConfigureOptions : IPostConfigureOptions<HttpOptions>
  {
    public void PostConfigure(string name, HttpOptions options)
    {
      var setResponse = options.SetResponse;

      options.SetResponse = (request, value) =>
      {
        if (value is IExecutionResult result)
        {
          if (result.HasError)
          {
            setResponse(request, new BadRequestResult());

            return;
          }

          if (result is IExecutionResult<object> resultWithContent)
          {
            setResponse(request, new OkObjectResult(resultWithContent.Result));

            return;
          }

          setResponse(request, new NoContentResult());

          return;
        }

        setResponse(request, value);
      };
    }
  }
}
