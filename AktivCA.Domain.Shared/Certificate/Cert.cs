using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktivCA.Domain.Shared.Certificate
{
    public class CertPem
    {
        public string? Pem { get; set; }
    }

    public class CertValidationResult
    {
        public bool IsValid { get; set; }
        public string? Reason { get; set; }
    }
}
