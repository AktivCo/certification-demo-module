using AktivCA.Application.Contracts.Setting;
using AktivCA.Domain.Settings;
using AktivCA.Application.Base;

namespace AktivCA.Application.Settings
{
    public class SettingsMapProfile : AutoMapper.Profile
    {
        public SettingsMapProfile() {
            CreateMap<SettingDto, Setting>().Ignore(x => x.Id);
        }
    }
}
