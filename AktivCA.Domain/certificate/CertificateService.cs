using AktivCA.Application.Contracts.certificate.dto;
using AktivCA.Domain.Shared.AutoReg;
using AktivCA.Domain.Shared.Exceptions;
using System.Security.Cryptography.X509Certificates;

//Todo
//using AktivCA.Domain.EntityFrameworkCore.EntityFrameworkCore;


namespace AktivCA.Domain.certificate
{
    //public class CertificateService(AktivCADbContext aktivCADbContext) : ICertificateService, ITransientService TODO
    public class CertificateService : ICertificateService, ITransientService
    {
        public CertValidationResult Validate(string certPem)
        {
            if(string.IsNullOrWhiteSpace(certPem))
                throw new CustomException("Некорректные данные");

            var userCert = X509Certificate2.CreateFromPem(certPem);
            var issuerCert = GetCaCert();

            var chain = new X509Chain();
            chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
            chain.ChainPolicy.CustomTrustStore.Add(issuerCert);
            chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

            var result = new CertValidationResult
            {
                IsValid = chain.Build(userCert),
                Reason = chain.ChainStatus.Length > 0 ? chain.ChainStatus[0].StatusInformation : null
            };

            return result;
        }

        private X509Certificate2 GetCaCert()
        {
            // Todo get from db
            const string pem = @"-----BEGIN CERTIFICATE-----
MIICJjCCAdOgAwIBAgIJAJaBFwcg/dDJMAoGCCqFAwcBAQMCMGsxDzANBgNVBAgM
Bk1vc2NvdzEPMA0GA1UEBwwGTW9zY293MRYwFAYDVQQKDA1BTyBBa3Rpdi1Tb2Z0
MRAwDgYDVQQLDAdSdXRva2VuMR0wGwYDVQQDDBRSdXRva2VuIFRFU1QgQ0EgR09T
VDAeFw0yMTExMDIxOTI3MzNaFw0zMTExMDIxOTI3MzNaMGsxDzANBgNVBAgMBk1v
c2NvdzEPMA0GA1UEBwwGTW9zY293MRYwFAYDVQQKDA1BTyBBa3Rpdi1Tb2Z0MRAw
DgYDVQQLDAdSdXRva2VuMR0wGwYDVQQDDBRSdXRva2VuIFRFU1QgQ0EgR09TVDBm
MB8GCCqFAwcBAQEBMBMGByqFAwICIwEGCCqFAwcBAQICA0MABECkPofhHt+UsPQK
6DUygI8bceGIM+MyFPfHrOcXtCFWx56ptOcVTQ3jvYQQPW5r0124POz4kLwXLW0g
i9OzLZ7Do1MwUTAfBgNVHSMEGDAWgBTz2dkNbD+wEB0Bu7SxAVaZBEeWcjAdBgNV
HQ4EFgQU89nZDWw/sBAdAbu0sQFWmQRHlnIwDwYDVR0TAQH/BAUwAwEB/zAKBggq
hQMHAQEDAgNBAEulwsLg/zE72Cxa6Ww70XVu/iobGrRJGcu5V96rtZpfaT4dRGQg
9Z+twz4X6DP9mk/ZQ0TALC7F0AaIuayNFL0=
-----END CERTIFICATE-----
";
            var result =  X509Certificate2.CreateFromPem(pem);

            if (result == null)
                throw new CustomException(null, "Отсутствует сертификат центра сертификации");

            return result;
        }
    }
}
