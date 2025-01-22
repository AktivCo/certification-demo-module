namespace AktivCA.Domain.Shared.Configuration
{
    public class CertificateParams
    {
        public bool IsRootCa { get; set; }
        public required string Name { get; set; }
        public int DurationInYears { get; set; }
        public int ChildDurationInYears { get; set; }
        public int IntermediateDurationInYears { get; set; }
        public Uri? CaUrl { get; set; }
    }
}
