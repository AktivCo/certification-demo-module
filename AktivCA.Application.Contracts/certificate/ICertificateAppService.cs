
using AktivCA.Application.Contracts.Certificate.Dto;
using Org.BouncyCastle.Pkcs;
using static System.Net.Mime.MediaTypeNames;

namespace AktivCA.Application.Contracts.Certificate
{
    public interface ICertificateAppService
    {
        public Task<PemContainerDto> Request(PemContainerDto request);
        public CertValidationResultDto Validate(PemContainerDto pem);
        public Task<PemCertResponseContainerDto> RequestIntermediate(PemContainerDto request);
    }
}
