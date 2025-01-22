using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Domain.Shared.Module;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using AktivCA.Domain.Settings;
using AktivCA.Domain.CAApi;

namespace AktivCA.Domain
{
    public class DomainModule : ApplicationModuleBase, IApplicationModuleBase
    {
        private ISettingService _settingService;
        public DomainModule(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public override void Init()
        {
            var settings = _settingService.GetSettings().Result;
            _settingService.InitSettings(settings).Wait();
        }

        //TODO: переместить сюда регистрацию ICAApiService
        public override void Init(IServiceCollection serviceCollection)
        {
            //serviceCollection
            //.AddHttpClient<ICAApiService, CAApiService>(httpClient =>
            //{
            //    httpClient.BaseAddress = certParamsSection.GetValue<Uri>(nameof(CertificateParams.CaUrl));
            //});
        }
    }
}
