using AktivCA.Application.Contracts.Certificate.Dto;
using AktivCA.Application.Contracts.Certificate;
using AktivCA.Application.Contracts.Setting;
using AktivCA.Domain.Certificate;
using AktivCA.Domain.Settings;
using AktivCA.Domain.Shared.AutoReg;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Pkcs;

namespace AktivCA.Application.Certificate
{
    [ApiController]
    [Route("certificate")]
    public class CertificateAppService : Controller, ICertificateAppService, ITransientService
    {
        private readonly IMapper _mapper;
        private readonly ICertificateService _certificateService;
        public CertificateAppService(
            IMapper mapper, ICertificateService certificateService)
        {
            _mapper = mapper;
            _certificateService = certificateService;
        }
   
        [Route("request")]
        [HttpPost]
        public async Task<PemContainerDto> Request([FromBody] PemContainerDto request)
        {
            var certRequest = await _certificateService.GetCertRequestFromCmsString(request.Pem);
            var cert = _certificateService.GenerateChildCertByRequest(certRequest);
            var pemCert = cert.ExportCertificatePem();
            return new PemContainerDto() { Pem = pemCert };
        }
      
        [Route("request-intermediate")]
        [HttpPost]
        public async Task<PemContainerDto> RequestIntermediate([FromBody] PemContainerDto request)
        {
            var certRequest = await _certificateService.GetCertRequestFromCmsString(request.Pem);
            var cert = _certificateService.GenerateCertByRequest(certRequest);
            var pemCert = cert.ExportCertificatePem();
            return new PemContainerDto() {Pem= pemCert };
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
