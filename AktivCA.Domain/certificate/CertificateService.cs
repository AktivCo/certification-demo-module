﻿using AktivCA.Domain.Shared.AutoReg;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.X509.Extension;
using AktivCA.Domain.Shared.Configuration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Pkcs;
using AktivCA.Domain.Settings;
using AktivCA.Domain.KeyPair;
using Org.BouncyCastle.Asn1;
using X509Extension = Org.BouncyCastle.Asn1.X509.X509Extension;
using System.Runtime.ConstrainedExecution;
using AktivCA.Domain.Shared.Exceptions;
using AktivCA.Domain.Shared.Certificate;

namespace AktivCA.Domain.Certificate
{
    public class CertificateService : ICertificateService, ITransientService
    {
        private IOptions<CertificateParams> _appSettings { get; set; }
        private ISettingProvider _settingProvider { get; set; }
        private IKeyPairService _keyPairService { get; set; }
        public CertificateService(
            IOptions<CertificateParams> appSettings,
            ISettingProvider settingProvider,
            IKeyPairService keyPairService
            )
        {
            _appSettings = appSettings;
            _settingProvider = settingProvider;
            _keyPairService = keyPairService;
        }

        public async Task<Pkcs10CertificationRequest> GetCertRequestFromCmsString(string сms)
        {
            Pkcs10CertificationRequest request;

            using (var _sr = new StringReader(сms))
            {
                var pRd = new PemReader(_sr);
                request = (Pkcs10CertificationRequest)pRd.ReadObject();
                pRd.Reader.Close();
            }

            return request;
        }

        public X509Certificate2 GenerateChildCertByRequest(Pkcs10CertificationRequest request)
        {
            request.Verify(request.GetPublicKey());
            var settings = GetSettings().Result;
            var keyPair = _keyPairService.GetKeyObjectFromStringKeys(settings.PrivateKey, settings.PublicKey);
            var caCert = new Org.BouncyCastle.X509.X509CertificateParser().ReadCertificate(X509Certificate2.CreateFromPem(settings.Cert).GetRawCertData());

            X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();

            var serialNumber = new Org.BouncyCastle.Math.BigInteger(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
            var name = String.Format(@"CN={0}", _appSettings.Value.Name);

            var issuerDN = new X509Name(name);
            var subjectDN = request.GetCertificationRequestInfo().Subject;

            var notBefore = DateTime.UtcNow.Date;
            var notAfter = notBefore.AddYears(_appSettings.Value.ChildDurationInYears);

            certificateGenerator.SetSerialNumber(serialNumber);

            certificateGenerator.SetIssuerDN(issuerDN); 
            certificateGenerator.SetSubjectDN(subjectDN);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            certificateGenerator.SetPublicKey(request.GetPublicKey());

            certificateGenerator.AddExtension(X509Extensions.AuthorityKeyIdentifier, false, new AuthorityKeyIdentifierStructure(caCert));
            certificateGenerator.AddExtension(X509Extensions.SubjectKeyIdentifier, false, new SubjectKeyIdentifierStructure(request.GetPublicKey()));
            certificateGenerator.AddExtension(X509Extensions.KeyUsage, false, new KeyUsage(KeyUsage.DigitalSignature));

            ISignatureFactory signatureFactory = new Asn1SignatureFactory(RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256.Id, keyPair.Private);
            var certificate = certificateGenerator.Generate(signatureFactory);
            certificate.Verify(keyPair.Public);
            var x509Certificate = new X509Certificate2(certificate.GetEncoded());
            return x509Certificate;
        }
        public X509Certificate2 GenerateCertByRequest(Pkcs10CertificationRequest request)
        {
            request.Verify(request.GetPublicKey());
            var settings = GetSettings().Result;
            var keyPair = _keyPairService.GetKeyObjectFromStringKeys(settings.PrivateKey, settings.PublicKey);
            var caCert = new Org.BouncyCastle.X509.X509CertificateParser().ReadCertificate(X509Certificate2.CreateFromPem(settings.Cert).GetRawCertData());

            X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();
            
            var serialNumber = new Org.BouncyCastle.Math.BigInteger(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
            var name = String.Format(@"CN={0}", _appSettings.Value.Name);

            var issuerDN = new X509Name(name);
            var subjectDN = request.GetCertificationRequestInfo().Subject;

            var notBefore = DateTime.UtcNow.Date;
            var notAfter = notBefore.AddYears(_appSettings.Value.IntermediateDurationInYears);

            certificateGenerator.SetSerialNumber(serialNumber);

            certificateGenerator.SetIssuerDN(issuerDN); 
            certificateGenerator.SetSubjectDN(subjectDN);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            certificateGenerator.SetPublicKey(request.GetPublicKey());

            certificateGenerator.AddExtension(X509Extensions.AuthorityKeyIdentifier, false, new AuthorityKeyIdentifierStructure(caCert));
            certificateGenerator.AddExtension(X509Extensions.SubjectKeyIdentifier, false, new SubjectKeyIdentifierStructure(request.GetPublicKey()));
            certificateGenerator.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(KeyUsage.KeyCertSign));
            certificateGenerator.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(true));
            ISignatureFactory signatureFactory = new Asn1SignatureFactory(RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256.Id, keyPair.Private);
            var certificate = certificateGenerator.Generate(signatureFactory);
            certificate.Verify(keyPair.Public);
            var x509Certificate = new X509Certificate2(certificate.GetEncoded());
            return x509Certificate;
        }

        public X509Certificate2 GenerateCertCA(AsymmetricCipherKeyPair keyPair)
        {
            X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();

            var serialNumber = new Org.BouncyCastle.Math.BigInteger(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
            var name = String.Format(@"CN={0}", _appSettings.Value.Name);
            var subjectDN = new X509Name(name);
            var issuerDN = new X509Name(name);
            var notBefore = DateTime.UtcNow.Date;
            var notAfter = notBefore.AddYears(_appSettings.Value.DurationInYears);

            certificateGenerator.SetSerialNumber(serialNumber);

            certificateGenerator.SetIssuerDN(issuerDN); 
            certificateGenerator.SetSubjectDN(subjectDN);


            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            certificateGenerator.SetPublicKey(keyPair.Public);

            certificateGenerator.AddExtension(X509Extensions.SubjectKeyIdentifier, false, new SubjectKeyIdentifierStructure(keyPair.Public));
            certificateGenerator.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(KeyUsage.KeyCertSign));
            certificateGenerator.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(true));

            ISignatureFactory signatureFactory = new Asn1SignatureFactory(RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256.ToString(), keyPair.Private);
            var certificate = certificateGenerator.Generate(signatureFactory);
            var x509Certificate = new X509Certificate2(certificate.GetEncoded());

            return x509Certificate;
        }

        public string GeneratePemCertRequest(AsymmetricCipherKeyPair keyPair, string name)
        {
            var csr = GenerateCertRequest(keyPair, name);
            var csrPem = GetPem(csr);
            return csrPem;
        }
        public Pkcs10CertificationRequest GeneratePkcs10CertRequest(AsymmetricCipherKeyPair keyPair, string name)
        {
            var csr = GenerateCertRequest(keyPair, name);
            return csr;
        }
        private Pkcs10CertificationRequest GenerateCertRequest(AsymmetricCipherKeyPair keyPair, string name)
        {
            // Информация о субъекте
            var subject = new X509Name($"CN={name}");

            // На текущий момент определяется CA
            // Создание CSR со специфическими расширениями
            var extensions = new Dictionary<DerObjectIdentifier, X509Extension>
            {
                //{
                //    X509Extensions.BasicConstraints,
                //    new X509Extension(true, new DerOctetString(new BasicConstraints(true)))
                //},
                //{
                //    X509Extensions.KeyUsage,
                //    new X509Extension(true, new DerOctetString(new KeyUsage(KeyUsage.KeyCertSign)))
                //}
            };

            var attributes = new DerSet(new AttributePkcs(
                PkcsObjectIdentifiers.Pkcs9AtExtensionRequest,
                new DerSet(new X509Extensions(extensions))));

            // Генерация запроса на сертификат
            var csr = new Pkcs10CertificationRequest(
                RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256.Id,
                subject,
                keyPair.Public,
                attributes,
                keyPair.Private);

            var csrPem = GetPem(csr);
            return csr;
        }

        public CertValidationResult Validate(string certPem)
        {
            var userCert = X509Certificate2.CreateFromPem(certPem);
            var settings = _settingProvider.GetSettingsAsync().Result;
            var issuerCert = X509Certificate2.CreateFromPem(settings.Cert);

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

        private string GetPem(object obj)
        {
            using var stringWriter = new StringWriter();
            var pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject(obj);
            pemWriter.Writer.Flush();
            var pem = stringWriter.ToString();

            return pem;
        }

        private async Task<Setting> GetSettings()
        {
            return await _settingProvider.GetSettingsAsync();
        }
    }
}
