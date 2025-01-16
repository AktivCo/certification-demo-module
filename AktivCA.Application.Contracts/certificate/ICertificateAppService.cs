using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Application.Contracts.certificate.dto;

namespace AktivCA.Application.Contracts.Certificate
{
    public interface ICertificateAppService
    {
        public Task Request(CmsRequest request);
        public Task RequestIntermediate(CmsRequest request);
        public Task Validate(string sertId);
    }
}
