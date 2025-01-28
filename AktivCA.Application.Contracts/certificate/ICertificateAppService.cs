
using AktivCA.Application.Contracts.Certificate.Dto;

namespace AktivCA.Application.Contracts.Certificate
{
    public interface ICertificateAppService
    {
        public Task<PemContainerDto> RequestCert(PemContainerDto request);
        public CertValidationResultDto Validate(PemContainerDto pem);
        public Task<PemCertResponseContainerDto> RequestIntermediate(PemContainerDto request);
    }
}
