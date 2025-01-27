namespace AktivCA.Domain.Shared.Certificate
{
    public class PemContainer
    {
        public string Pem { get; set; }
    }
    public class PemCertResponseContainer
    {
        public string Pem { get; set; }
        public string CaPem { get; set; }
    }
}
