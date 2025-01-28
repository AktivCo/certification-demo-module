using AktivCA.Application.Contracts.Certificate.Dto;
using AktivCA.Domain.Shared.Certificate;

namespace AktivCA.Application.Certificate
{
    public class CertificateMapProfile : AutoMapper.Profile
    {
        public CertificateMapProfile()
        {
            CreateMap<CertValidationResult, CertValidationResultDto>();
            CreateMap<CertValidationResultDto, CertValidationResult>();
            CreateMap<PemCertResponseContainer, PemCertResponseContainerDto>();
            CreateMap<PemCertResponseContainerDto, PemCertResponseContainer>();
        }
    }
}
