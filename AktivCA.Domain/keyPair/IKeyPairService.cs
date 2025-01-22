using AktivCA.Domain.Shared.AutoReg;
using Org.BouncyCastle.Crypto;

namespace AktivCA.Domain.KeyPair
{
    public interface IKeyPairService : ITransientService
    {
        public AsymmetricCipherKeyPair GenerateKeyPair();
        public AsymmetricCipherKeyPair GetKeyObjectFromStringKeys(string privateKeyString, string publicKeyString);
    }
}
