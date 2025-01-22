using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
