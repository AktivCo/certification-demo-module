using AktivCA.Application.Contracts.Certificate.Dto;
using AktivCA.Application.Contracts.Certificate;
using AktivCA.Domain.Certificate;
using AktivCA.Domain.Settings;
using AktivCA.Domain.Shared.AutoReg;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AktivCA.Application.Certificate
{
    [ApiController]
    [Route("certificate")]
    public class CertificateAppService : Controller, ICertificateAppService, ITransientService
    {
        private readonly IMapper _mapper;
        private readonly ICertificateService _certificateService;
        private readonly ISettingService _settingService;
        public CertificateAppService(
            IMapper mapper, ICertificateService certificateService, ISettingService settingService)
        {
            _mapper = mapper;
            _certificateService = certificateService;
            _settingService = settingService;
        }
   
        [Route("request")]
        [HttpPost]
        public async Task<PemContainerDto> RequestCert([FromBody] PemContainerDto request)
        {
            var certRequest = _certificateService.GetCertRequestFromCmsString(request.Pem);
            var cert = await _certificateService.GenerateChildCertByRequest(certRequest);
            var pemCert = cert.ExportCertificatePem();
            return new PemContainerDto() { Pem = pemCert };
        }

        [Route("request-intermediate")]
        [HttpPost]
        public async Task<PemCertResponseContainerDto> RequestIntermediate([FromBody] PemContainerDto request)
        {
            var certRequest = _certificateService.GetCertRequestFromCmsString(request.Pem);
            var cert = await _certificateService.GenerateCertByRequest(certRequest);
            var pemCert = cert.ExportCertificatePem();
            var settings = await _settingService.GetSettings();
            return new PemCertResponseContainerDto() { Pem = pemCert, CaPem = settings.CaCert };
        }

        [Route("validate")]
        [HttpPost]
        public CertValidationResultDto Validate([FromBody] PemContainerDto model)
        {
            var result = _certificateService.Validate(model.Pem);
            return _mapper.Map<CertValidationResultDto>(result);
        }
    }
}
