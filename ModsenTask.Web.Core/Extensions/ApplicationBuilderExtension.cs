using Microsoft.AspNetCore.Builder;
using ModsenTask.Web.Core.MiddleWares;

namespace ModsenTask.Web.Core.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ApplyCustomMiddlewares(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.ExceptionMiddleware();
    
    public static IApplicationBuilder ExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<ExceptionMiddleware>();
}