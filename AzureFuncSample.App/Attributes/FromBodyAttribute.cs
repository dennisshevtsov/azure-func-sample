using System;

using Microsoft.Azure.WebJobs.Description;

namespace AzureFuncSample.App.Attributes
{
  [Binding]
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  public sealed class FromBodyAttribute : Attribute { }
}
