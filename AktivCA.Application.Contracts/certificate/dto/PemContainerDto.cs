using System.ComponentModel.DataAnnotations;

namespace AktivCA.Application.Contracts.Certificate.Dto
{
    public class PemContainerDto
    {
        [Required]
        public required string Pem { get; set; }
    }
    public class PemCertResponseContainerDto
    {
        [Required]
        public required string Pem { get; set; }
        [Required]
        public required string CaPem { get; set; }
    }
}
