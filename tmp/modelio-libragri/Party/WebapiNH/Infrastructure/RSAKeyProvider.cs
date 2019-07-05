using System;
using System.IO;
using System.Security.Cryptography;
using Common.Crypto;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace Libragri.PartyDomain.Webapi
{

    public class PrivateKey
    {
        public DateTime LastUpdatedTime { get; set; }
        public RSACryptoServiceProvider PrivateKeyRSAProvider { get; set; }
    }

    public class RSAKeyProvider
    {
        private static PrivateKey _privateKey;

        private static object _syncLock = new object();



        public static RSACryptoServiceProvider GetPrivateKey()
        {
            string pemFilename = "./PrivateKey.pem";
            string publicFilename = "./PublicKey.pem";
            if (_privateKey==null )
            {
                lock(_syncLock)
                {
                    if(_privateKey==null )
                    {
                        _privateKey = new PrivateKey();
                        _privateKey.PrivateKeyRSAProvider=new RSACryptoServiceProvider(2048);
                        if (File.Exists(pemFilename))
                        {
                            
                            var lastUpdatedTime = File.GetLastWriteTimeUtc(pemFilename);
                            using (var fileStream = File.OpenText(pemFilename))
                            {
                                var pemReader = new PemReader(fileStream);
                                var KeyParameter = (RsaPrivateCrtKeyParameters)((AsymmetricCipherKeyPair)pemReader.ReadObject()).Private;
                                var rsaParameter = DotNetUtilities.ToRSAParameters(KeyParameter);
                               
                                _privateKey.PrivateKeyRSAProvider.ImportParameters(rsaParameter);
                                _privateKey.LastUpdatedTime = lastUpdatedTime;
                            }
                        }
                        else
                        {
                            
                            string privatekey = RSAKeysGenerator.ExportPrivateKey(_privateKey.PrivateKeyRSAProvider);
                            _privateKey.LastUpdatedTime = DateTime.Now;
                            string publickey = RSAKeysGenerator.ExportPublicKey(_privateKey.PrivateKeyRSAProvider);
                            
                            File.WriteAllText(pemFilename,privatekey);
                            File.WriteAllText(publicFilename,publickey);
                        }
                    }
                }
            }
            return _privateKey.PrivateKeyRSAProvider;
        }
    }
}