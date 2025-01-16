using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Application.Contracts.certificate.dto;
using AktivCA.Application.Contracts.Certificate;
using AktivCA.Application.Contracts.setting;
using AktivCA.Domain.settings;
using AktivCA.Domain.Shared.AutoReg;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AktivCA.Application.Certificate
{
    [Route("certificate")]
    public class CertificateAppService : Controller, ICertificateAppService, ITransientAppService
    {
        private readonly IMapper _mapper;
        public CertificateAppService(IMapper mapper)
        {
            _mapper = mapper;
        }
   
        [Route("request")]
        [HttpPost]
        public Task Request([FromBody] CmsRequest request)
        {
            throw new NotImplementedException(); 
        }

        [Route("requestintermediate")]
        [HttpPost]
        public Task RequestIntermediate(CmsRequest request)
        {
            throw new NotImplementedException();
        }

        [Route("validate/{sertId}")]
        [HttpGet]
        public Task Validate(string sertId)
        {
            var test = new SettingDto() {Id = 1, Cert="testCert", PrivateKey = "PrivateKey", PublicKey = "PublicKey" };
            var testResult = _mapper.Map<Setting>(test);
            throw new NotImplementedException();
        }

    }
}
