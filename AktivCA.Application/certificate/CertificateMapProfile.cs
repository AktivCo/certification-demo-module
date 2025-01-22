using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Application.Base;
using AktivCA.Application.Contracts.Certificate.Dto;
using AktivCA.Application.Contracts.Setting;
using AktivCA.Domain.Certificate;
using AktivCA.Domain.Settings;
using AktivCA.Domain.Shared.Certificate;
using Org.BouncyCastle.Pkcs;

namespace AktivCA.Application.Certificate
{
    public class CertificateMapProfile : AutoMapper.Profile
    {
        public CertificateMapProfile()
        {
            CreateMap<CertValidationResult, CertValidationResultDto>();
            CreateMap<CertValidationResultDto, CertValidationResult>();
        }
    }
}
