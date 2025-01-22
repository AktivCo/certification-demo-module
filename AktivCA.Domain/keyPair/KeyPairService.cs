using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace AktivCA.Domain.KeyPair
{
    public class KeyPairService : IKeyPairService
    {
        public AsymmetricCipherKeyPair GenerateKeyPair()
        {
            var oid = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256_paramSetA;
            var ecp = new ECNamedDomainParameters(oid, ECGost3410NamedCurves.GetByOid(oid));
            var gostParams = new ECGost3410Parameters(ecp, oid, RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256, null);
            var parameters = new ECKeyGenerationParameters(gostParams, new SecureRandom());
            var engine = new ECKeyPairGenerator();
            engine.Init(parameters);

            var keyPair = engine.GenerateKeyPair();

            return keyPair;
        }

        public AsymmetricCipherKeyPair GetKeyObjectFromStringKeys(string privateKeyString, string publicKeyString)
        {
            AsymmetricKeyParameter privateKey;
            AsymmetricKeyParameter publicKey;
            using (var _sr = new StringReader(privateKeyString))
            {
                var pRd = new PemReader(_sr);
                privateKey = (AsymmetricKeyParameter)pRd.ReadObject();
                pRd.Reader.Close();
            }
            using (var _sr = new StringReader(publicKeyString))
            {
                var pRd = new PemReader(_sr);
                publicKey = (AsymmetricKeyParameter)pRd.ReadObject();
                pRd.Reader.Close();
            }

            AsymmetricCipherKeyPair keyPair =
                    new AsymmetricCipherKeyPair(
                        publicKey,
                        privateKey);
            return keyPair;
        }
    }
}
