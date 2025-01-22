namespace AktivCA.Domain.Shared.Module
{
    public class CmsRequest
    {
        public string Cms { get; set; }
        public string ObjectId { get; set; }
        public string CrlLink { get; set; }
        public string RootCertLink { get; set; }
    }
}