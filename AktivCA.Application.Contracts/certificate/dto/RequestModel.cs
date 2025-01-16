using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X509;

namespace AktivCA.Application.Contracts.certificate.dto
{
    public class RequestModel
    {
        public string Name { get; set; }
        public int SerialNumber { get; set; }
        public string SubjectDN { get; set; }
        public string IssuerDN { get; set; }
        public DateTime NotBeforeDate { get; set; }
        public DateTime NotAfterDate { get; set; }
        public string PublicKey { get; set; }
        public string SignatureAlgorithm { get; set; }
        public X509Extensions[] Extensions { get;set; }
    }
}
