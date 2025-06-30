using AktivCA.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AktivCA.Application.Attributes
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute() : base(typeof(ApiKeyAuthorizationFilter)) { }
    }
}
