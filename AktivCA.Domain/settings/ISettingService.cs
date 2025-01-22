using AktivCA.Domain.Shared.AutoReg;
using Org.BouncyCastle.Crypto;

namespace AktivCA.Domain.Settings
{
    public interface ISettingService : ITransientService
    {
        public Task InitSettings(Setting? settings);
        public Task<Setting> GetSettings();
       
    }
}
