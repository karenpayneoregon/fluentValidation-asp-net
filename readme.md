# FluentValidation Asp.Net Core

This solution demonstrates how to integrate FluentValidation with an ASP.NET Core Web API project. It includes examples of setting up validators, configuring the application to use FluentValidation, and handling validation errors.


💡 Code presented has no controller but should be easy to integrate with controllers.

## Article

https://dev.to/karenpayneoregon/aspnet-core-fluentvalidation-16nc

## NuGet Packages

[FluentValidation.AspNetCore](https://www.nuget.org/packages/FluentValidation.AspNetCore/11.3.1?_src=template) This package has been deprecated as it is legacy and is no longer maintained. Still many developers use it which down the road can cause issues. It is recommended to use the new packages

- [FluentValidation](https://www.nuget.org/packages/FluentValidation) 
- [FluentValidation.DependencyInjectionExtensions](https://www.nuget.org/packages/FluentValidation.DependencyInjectionExtensions/11.3.1?_src=template).


If transitioning from FluentValidation.AspNetCore the following lanaguage extensions will needed which is in the project `WebValidationLibrary1`

```csharp
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;

public static class Extensions
{

    /// <summary>
    /// Adds the validation errors from a <see cref="ValidationResult"/> to the specified <see cref="ModelStateDictionary"/>.
    /// </summary>
    /// <param name="result">The <see cref="ValidationResult"/> containing validation errors.</param>
    /// <param name="modelState">The <see cref="ModelStateDictionary"/> to which the validation errors will be added.</param>
    /// <param name="prefix">
    /// An optional prefix to prepend to the property names of the validation errors. 
    /// If the prefix is not provided, the property names will be used as-is.
    /// </param>
    /// <remarks>
    /// This method iterates through the validation errors in the <paramref name="result"/> and adds them to the 
    /// <paramref name="modelState"/>. If a prefix is specified, it is prepended to the property names of the errors.
    /// </remarks>
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState, string prefix)
    {
        
        if (result.IsValid) return;
        
        foreach (var error in result.Errors)
        {
            string key = string.IsNullOrEmpty(prefix)
                ? error.PropertyName
                : string.IsNullOrEmpty(error.PropertyName)
                    ? prefix
                    : $"{prefix}.{error.PropertyName}";
            modelState.AddModelError(key, error.ErrorMessage);
        }
    }

}
```