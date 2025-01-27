using AktivCA.Domain.Shared.Certificate;

namespace AktivCA.Domain.CAApi
{
    public interface ICAApiService
    {
        Task<PemCertResponseContainer> CreateCertAsync(string cmsRequest);
    }
}
