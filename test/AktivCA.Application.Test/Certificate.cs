using AktivCA.Application.Contracts.Certificate.Dto;
using AktivCA.Application.Contracts.Certificate;
using AktivCA.Test.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AktivCA.Application.Test
{
    public class Certificate: TestBase
    {
        [SetUp]
        public void Setup()
        {
            RegisterDI();
        }

        [Test]
        public void RequestIntermediateString()
        {
            string cert = @"-----BEGIN NEW CERTIFICATE REQUEST-----
MIIBLzCB3QIBADAZMRcwFQYDVQQDDA5xd3Jxd3J3cXJxd3JxdzBmMB8GCCqFAwcB
AQEBMBMGByqFAwICJAAGCCqFAwcBAQICA0MABECkRm9zgHexn1+UrREmdAFLWDw1
5q8Vd78MoFcKUWm8bemMA6URCbidz1wFHnUzfLEhxBkzGTJWxv8o1ialkm4loFUw
UwYJKoZIhvcNAQkOMUYwRDAOBgNVHQ8BAf8EBAMCBPAwEwYDVR0lBAwwCgYIKwYB
BQUHAwIwHQYDVR0OBBYEFIQ4wYErhIcfyP3BFHLTvFpQgWvjMAoGCCqFAwcBAQMC
A0EALtxP0wh1QZsHa7xxVyVEtZkhC85XRuUA5ggTeNO83sY4fW/ZM8WZmQ+ZsUFf
bqklF9K5r/QQfGhzmSPNChayeQ==
-----END NEW CERTIFICATE REQUEST-----";
            var certificateService = _serviceProvider.GetService<ICertificateAppService>();

            certificateService.RequestIntermediate(new PemContainerDto() { Pem = cert }).GetAwaiter().GetResult();

            Assert.Pass();
        }

        [Test]
        public void ValidateChildCert()
        {
            string cert = @"-----BEGIN CERTIFICATE-----
MIIBvDCCAWegAwIBAgIGAZSSp9iPMAwGCCqFAwcBAQMCBQAwITEfMB0GA1UEAwwW
UnV0b2tlbiBUZXN0IENBIEdPU1QgSTAeFw0yNTAxMjMwMDAwMDBaFw0yNjAxMjMw
MDAwMDBaMB8xHTAbBgNVBAMMFFJ1dG9rZW4gVGVzdCBDQSBHT1NUMGgwIQYIKoUD
BwEBAQEwFQYJKoUDBwECAQEBBggqhQMHAQECAgNDAARA+p8BMJwVFLUSBxTtIKq3
OylbzOiECuZj+/pneSinWk9WFz8WHv8aG+Ecqy6cPlyTOxqNJB2nfFyh8u3ENJ36
eaN8MHowTAYDVR0jBEUwQ4AUmnicWt4cHyrcWgWrsBw1ppr4gL+hI6QhMB8xHTAb
BgNVBAMMFFJ1dG9rZW4gVGVzdCBDQSBHT1NUggYBlJKjNpcwHQYDVR0OBBYEFIsP
Y617f82fSWYuRoYK88vSw+RDMAsGA1UdDwQEAwIHgDAMBggqhQMHAQEDAgUAA0EA
B6txDqxda00DHo6MyaYV8xVPNWqVE7vy9ZNN1t4CooIzNJVm/x+9bZVlYxY7iDoY
erFSRL4vxRAlTRaWPkf/7g==
-----END CERTIFICATE-----";
            var certificateService = _serviceProvider.GetService<ICertificateAppService>();

            var t = certificateService.Validate(new PemContainerDto() { Pem = cert });

            Assert.Pass();
        }
    }
}