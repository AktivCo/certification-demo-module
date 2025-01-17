using System;
using System.Collections.Generic;
using AktivCA.Application.Contracts.certificate.dto;
using AktivCA.Application.Contracts.Certificate;
using AktivCA.Application.Contracts.setting;
using AktivCA.Domain.certificate;
using AktivCA.Domain.settings;
using AktivCA.Domain.Shared.AutoReg;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AktivCA.Application.Certificate
{
    [Route("certificate")]
    public class CertificateAppService : Controller, ICertificateAppService, ITransientAppService
    {
        private readonly IMapper _mapper;
        private readonly ICertificateService _certService;

        public CertificateAppService(IMapper mapper, ICertificateService certService)
        {
            _mapper = mapper;
            _certService = certService;
        }
   
        [Route("request")]
        [HttpPost]
        public Task Request([FromBody] CmsRequest request)
        {
            throw new NotImplementedException(); 
        }

        [Route("request-intermediate")]
        [HttpPost]
        public Task RequestIntermediate(CmsRequest request)
        {
            throw new NotImplementedException();
        }

        [Route("validate")]
        [HttpPost]
        public CertValidationResult Validate([FromBody] CertPem model)
        {
            return _certService.Validate(model.Pem);
        }
    }
}
