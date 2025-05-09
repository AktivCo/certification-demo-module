using System.Security.Cryptography.X509Certificates;
using AktivCA.Domain.Shared.Certificate;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;

namespace AktivCA.Domain.Certificate
{
    public interface ICertificateService
    {
        public Task<CertValidationResult> Validate(string certPem);
        public Pkcs10CertificationRequest GetCertRequestFromCmsString(string сms);
        public X509Certificate2 GenerateCertCA(AsymmetricCipherKeyPair keyPair);
        public Task<X509Certificate2> GenerateCertByRequest(Pkcs10CertificationRequest request);
        public Task<X509Certificate2> GenerateChildCertByRequest(Pkcs10CertificationRequest request);
        public string GeneratePemCertRequest(AsymmetricCipherKeyPair keyPair, string name);
        public Pkcs10CertificationRequest GeneratePkcs10CertRequest(AsymmetricCipherKeyPair keyPair, string name);
    }
}
