using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.ResponseModels;

namespace Services.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private static ILogger<ExceptionHandlingMiddleware> loggerFactory;

        public ExceptionHandlingMiddleware(RequestDelegate Next, ILogger<ExceptionHandlingMiddleware> Logger)
        {
            next = Next;
            loggerFactory = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (!httpContext.Response.HasStarted)
                {
                    loggerFactory.LogError(ex, "Request Error");

                    httpContext.Response.StatusCode = 200;
                    httpContext.Response.ContentType = "application/json";
                    var response = new ServiceResponse<String>();
                    response.SetException(ex);
                    var json = JsonConvert.SerializeObject(response);
                    await httpContext.Response.WriteAsync(json);
                }
            }
        }
    }
}
