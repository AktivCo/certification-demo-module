using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Application.Contracts.setting;
using AktivCA.Domain.settings;
using AktivCA.Application.Base;

namespace AktivCA.Application.settings
{
    public class SettingsMapProfile : AutoMapper.Profile
    {
        public SettingsMapProfile() {
            CreateMap<SettingDto, Setting>().Ignore(x => x.Id);
        }
    }
}
