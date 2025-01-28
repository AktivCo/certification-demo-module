using AktivCA.Domain.Shared.AutoReg;

namespace AktivCA.Domain.Settings
{
    public interface ISettingProvider : ITransientService
    {
        public Task<Setting?> GetSettingsAsync();
        public Task CreateSettingsAsync(Setting entity);
        public void UpdateSettings(Setting entity);
        public Task SaveAsync();
    }
}
