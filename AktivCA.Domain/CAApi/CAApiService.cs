using System.Net.Http.Json;
using AktivCA.Domain.Shared.Certificate;

namespace AktivCA.Domain.CAApi
{
    public class CAApiService(HttpClient httpClient) : ICAApiService
    {
        public async Task<PemCertResponseContainer> CreateCertAsync(string cmsRequest)
        {
            using var httpResponseMessage =
                await httpClient.PostAsJsonAsync("/certificate/request-intermediate", new PemContainer() { Pem = cmsRequest });

            httpResponseMessage.EnsureSuccessStatusCode();

            var result = await httpResponseMessage.Content.ReadFromJsonAsync<PemCertResponseContainer>();
            return result;
        }
    }
}
