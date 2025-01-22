using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AktivCA.Domain.Settings
{
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Cert { get; set; }
    }
}
