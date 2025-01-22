using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Domain.Certificate;
using AktivCA.Domain.EntityFrameworkCore.EntityFrameworkCore;
using AktivCA.Domain.KeyPair;
using AktivCA.Domain.Settings;
using AktivCA.Domain.Shared.Configuration;
using AktivCA.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AktivCA.Domain.Test
{
    public class Settings: TestBase
    {
        [SetUp]
        public void Setup()
        {
            RegisterDI();
        }

        //[Test]
        //public async Task InitEmptySettings()
        //{
        //    var certificateService = _serviceProvider.GetService<ICertificateService>();
        //    var appSettings = _serviceProvider.GetService<IOptions<CertificateParams>>();
        //    var keyPairService = _serviceProvider.GetService<IKeyPairService>();
        //    var service = new SettingService(new SettingEmptyProvider(), certificateService, appSettings, keyPairService);
        //    var settings = await service.GetSettings();
        //    await service.InitSettings(settings);
        //    Assert.Pass();
        //}

        //[Test]
        //public async Task InitSettingsWithoutCert()
        //{
        //    var certificateService = _serviceProvider.GetService<ICertificateService>();
        //    var appSettings = _serviceProvider.GetService<IOptions<CertificateParams>>();
        //    var keyPairService = _serviceProvider.GetService<IKeyPairService>();
        //    var cert = new SettingCustomProvider(
        //        new Setting()
        //        {
        //            PrivateKey = "-----BEGIN PRIVATE KEY-----\r\nMEgCAQAwHwYIKoUDBwEBAQEwEwYHKoUDAgIjAQYIKoUDBwEBAgIEIgQgRooaqCHa\r\nrT6TWuW7vVKC6wBjm96RK/x5kfouUZxemmE=\r\n-----END PRIVATE KEY-----\r\n",
        //            PublicKey = "-----BEGIN PUBLIC KEY-----\r\nMGYwHwYIKoUDBwEBAQEwEwYHKoUDAgIjAQYIKoUDBwEBAgIDQwAEQKQb8LEdEBV3\r\njr69CYoS3BpeT6p6Sips9x9AFnRdlJZDZoMxfSqNJWiQqA+ra1NYF5y+elGBvfH2\r\nOV+Rw29geCE=\r\n-----END PUBLIC KEY-----\r\n"
        //        }
        //        );
        //    var service = new SettingService(cert, certificateService, appSettings, keyPairService);
        //    var settings = await service.GetSettings();
        //    await service.InitSettings(settings);
        //    Assert.Pass();
        //}
        //public class SettingEmptyProvider : ISettingProvider
        //{

        //    public async Task<Setting?> GetSettingsAsync()
        //    {
        //        return null;
        //    }

        //    public async Task CreateSettingsAsync(Setting entity)
        //    {

        //    }

        //    public void UpdateSettings(Setting entity)
        //    {

        //    }

        //    public async Task SaveAsync()
        //    {

        //    }
        //}
        //public class SettingCustomProvider : ISettingProvider
        //{
        //    private Setting _set { get; set; }
        //    public SettingCustomProvider(Setting set)
        //    {
        //        _set = set;
        //    }
        //    public async Task<Setting?> GetSettingsAsync()
        //    {
        //       return _set;
        //    }

        //    public async Task CreateSettingsAsync(Setting entity)
        //    {
                
        //    }

        //    public void UpdateSettings(Setting entity)
        //    {
               
        //    }

        //    public async Task SaveAsync()
        //    {
                
        //    }
        //}
    }
}
