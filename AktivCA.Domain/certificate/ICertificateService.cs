using AktivCA.Application.Contracts.certificate.dto

namespace AktivCA.Domain.certificate
{
    public interface ICertificateService
    {
        CertValidationResult Validate(string certPem);
    }
}
