using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AktivCA.Domain.Certificate;
using AktivCA.Domain.KeyPair;
using AktivCA.Test.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AktivCA.Domain.Test
{
    public class KeyPair: TestBase
    {
        [SetUp]
        public void Setup()
        {
            RegisterDI();
        }

        [Test]
        public void GenerateKeys()
        {
            var keyPairService = _serviceProvider.GetService<IKeyPairService>();
            var res = keyPairService.GenerateKeyPair();
            Assert.Pass();
        }
    }
}
