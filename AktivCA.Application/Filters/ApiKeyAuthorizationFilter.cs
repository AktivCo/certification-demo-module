using AktivCA.Application.Validators;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using AktivCA.Domain.Shared.AutoReg;

namespace AktivCA.Application.Filters
{
    public class ApiKeyAuthorizationFilter(IApiKeyValidator apiKeyValidator) : IAuthorizationFilter, ISelfScopedService
    {
        private const string ApiKeyHeaderName = "X-API-Key";

        private readonly IApiKeyValidator _apiKeyValidator = apiKeyValidator;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey) || !_apiKeyValidator.IsValid(apiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
