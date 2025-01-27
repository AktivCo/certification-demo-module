
using System.Security.Cryptography.X509Certificates;
using AktivCA.Domain.CAApi;
using AktivCA.Domain.Certificate;
using AktivCA.Domain.KeyPair;
using AktivCA.Domain.Shared.Configuration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;

namespace AktivCA.Domain.Settings
{
    public class SettingService : ISettingService
    {
        private ISettingProvider _settingProvider;
        private ICertificateService _certificateService;
        private IKeyPairService _keyPairService;
        private ICAApiService _cAApiService;
        private IOptions<CertificateParams> _appSettings { get; set; }
        public SettingService(
            ISettingProvider settingProvider, 
            ICertificateService certificateService, 
            IOptions<CertificateParams> appSettings,
            IKeyPairService keyPairService,
            ICAApiService cAApiService
            )
        {
            _settingProvider = settingProvider;
            _certificateService = certificateService;
            _appSettings = appSettings;
            _keyPairService = keyPairService;
            _cAApiService = cAApiService;
        }
        public async Task InitSettings(Setting? settings)
        {
            if (settings == null)
            {
                settings = new Setting();
                var keyPair = _keyPairService.GenerateKeyPair();
                FillKeysToSettings(settings, keyPair);

                if (_appSettings.Value.IsRootCa)
                {
                    X509Certificate2 cert = _certificateService.GenerateCertCA(keyPair);
                    var certString = cert.ExportCertificatePem();
                    settings.Cert = certString;
                    settings.CaCert = certString;
                }
                else {
                    var request = _certificateService.GeneratePemCertRequest(keyPair, _appSettings.Value.Name);
                    var result = _cAApiService.CreateCertAsync(request).Result;
                    settings.Cert = result.Pem;
                    settings.CaCert = result.CaPem;
                }

                await _settingProvider.CreateSettingsAsync(settings);
                await _settingProvider.SaveAsync();
            }
        }

        public async Task<Setting> GetSettings()
        {
            return await _settingProvider.GetSettingsAsync();
        }

        private void FillKeysToSettings(Setting settings, AsymmetricCipherKeyPair keyPair)
        {
            using (StringWriter sW = new StringWriter())
            {
                PrivateKeyInfo info = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);
                new PemWriter(sW).WriteObject(info);
                settings.PrivateKey = sW.ToString();
            }
            using (StringWriter sW = new StringWriter())
            {
                SubjectPublicKeyInfo info = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);
                new PemWriter(sW).WriteObject(info);
                settings.PublicKey = sW.ToString();
            }
        }
    }
}
