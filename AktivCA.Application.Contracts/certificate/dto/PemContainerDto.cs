using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktivCA.Application.Contracts.Certificate.Dto
{
    public class PemContainerDto
    {
        [Required]
        public string Pem { get; set; }
    }
    public class PemCertResponseContainerDto
    {
        [Required]
        public string Pem { get; set; }
        [Required]
        public string CaPem { get; set; }
    }
}
