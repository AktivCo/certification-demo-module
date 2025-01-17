
using AktivCA.Application.Contracts.certificate.dto;

namespace AktivCA.Application.Contracts.Certificate
{
    public interface ICertificateAppService
    {
        public Task Request(CmsRequest request);
        public Task RequestIntermediate(CmsRequest request);
        public CertValidationResult Validate(CertPem pem);
    }
}
