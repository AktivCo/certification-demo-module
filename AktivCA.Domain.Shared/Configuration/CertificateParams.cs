namespace AktivCA.Domain.Shared.Configuration
{
    public class CertificateParams
    {
        public bool IsRootCa { get; set; }
        public required string Name { get; set; }
        public int RootCertDurationInYears { get; set; }
        public int UserCertDurationInYears { get; set; }
        public int IntermediateDurationInYears { get; set; }
        public Uri? CaUrl { get; set; }
        public required string CurrentCaApiKey { get; set; }
        public string? ParentCaApiKey { get; set; }
    }
}
