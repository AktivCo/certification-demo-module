using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktivCA.Application.Contracts.Setting
{
    public class SettingDto
    {
        public int Id { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Cert { get; set; }
    }
}
