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
    }
}
