using AktivCA.Domain.Shared.AutoReg;
using AktivCA.Domain.Shared.Configuration;
using Microsoft.Extensions.Configuration;

namespace AktivCA.Application.Validators
{
    public interface IApiKeyValidator
    {
        bool IsValid(string? apiKey);
    }

    public class ApiKeyValidator(IConfiguration configuration) : IApiKeyValidator, IScopedService
    {
        public bool IsValid(string? apiKey)
        {
            if (apiKey is null)
            {
                return false;
            }

            var value = configuration[$"{nameof(CertificateParams)}:{nameof(CertificateParams.CurrentCaApiKey)}"];

            return value?.Equals(apiKey) ?? false;
        }
    }
}
