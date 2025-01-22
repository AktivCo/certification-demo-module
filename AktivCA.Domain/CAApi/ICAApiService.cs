using AktivCA.Domain.Shared.Module;
using Org.BouncyCastle.Pkcs;

namespace AktivCA.Domain.CAApi
{
    public interface ICAApiService
    {
        Task<string> CreateCertAsync(string cmsRequest);
    }
}
