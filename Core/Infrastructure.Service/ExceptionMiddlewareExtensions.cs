using Infrastructure.Application.BasicDto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Service
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (contextFeature != null)
                    {
                        ApiResponse<object> apiResponse = ErrorDetails<object>.CreateErrorResponse(contextFeature.Error);
                        context.Response.StatusCode = apiResponse.StatusCode;
                        await context.Response.WriteAsync(apiResponse.ToString());
                    }
                });
            });
        }
    }
}
