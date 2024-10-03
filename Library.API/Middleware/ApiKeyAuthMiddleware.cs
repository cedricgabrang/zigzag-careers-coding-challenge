using Library.Shared.Constants;
using System.Net;

namespace Library.API.Middleware
{
    public class ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            var apiKey = configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

            if (string.IsNullOrEmpty(apiKey) || !apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await next(context);

        }
    }
}
