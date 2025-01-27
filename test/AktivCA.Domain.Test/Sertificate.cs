using AktivCA.Domain.Certificate;
using AktivCA.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AktivCA.Domain.Test
{
    public class Certificate : TestBase
    {
        [SetUp]
        public void Setup()
        {
            RegisterDI();
        }

        [Test]
        public void ParseCertificateString()
        {
            var certificateService = _serviceProvider.GetService<ICertificateService>();
            string cert = @"-----BEGIN NEW CERTIFICATE REQUEST-----
MIIBLzCB3QIBADAZMRcwFQYDVQQDDA5xd3Jxd3J3cXJxd3JxdzBmMB8GCCqFAwcB
AQEBMBMGByqFAwICJAAGCCqFAwcBAQICA0MABECkRm9zgHexn1+UrREmdAFLWDw1
5q8Vd78MoFcKUWm8bemMA6URCbidz1wFHnUzfLEhxBkzGTJWxv8o1ialkm4loFUw
UwYJKoZIhvcNAQkOMUYwRDAOBgNVHQ8BAf8EBAMCBPAwEwYDVR0lBAwwCgYIKwYB
BQUHAwIwHQYDVR0OBBYEFIQ4wYErhIcfyP3BFHLTvFpQgWvjMAoGCCqFAwcBAQMC
A0EALtxP0wh1QZsHa7xxVyVEtZkhC85XRuUA5ggTeNO83sY4fW/ZM8WZmQ+ZsUFf
bqklF9K5r/QQfGhzmSPNChayeQ==
-----END NEW CERTIFICATE REQUEST-----";

            var result = certificateService.GetCertRequestFromCmsString(cert);

            Assert.Pass();
        }
    }
}