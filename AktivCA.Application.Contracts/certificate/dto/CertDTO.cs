
namespace AktivCA.Application.Contracts.Certificate.Dto
{
    public class CertValidationResultDto
    {
        public bool IsValid { get; set; }
        public string? Reason { get; set; }
    }
}