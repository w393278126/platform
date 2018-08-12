using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Xn.Platform.Extensions
{
    public class RSAForJava
    {

        /// <summary>
        /// RSA验签
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="publicKey">RSA公钥</param>
        /// <param name="signData">签名字段</param>
        /// <returns></returns>
        public static bool VerifySign(string content, string publicKey, string signData)
        {
            var signer = SignerUtilities.GetSigner("SHA1withRSA");
            var publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            signer.Init(false, publicKeyParam);
            var signBytes = Convert.FromBase64String(signData);
            var plainBytes = Encoding.UTF8.GetBytes(content);
            signer.BlockUpdate(plainBytes, 0, plainBytes.Length);
            var ret = signer.VerifySignature(signBytes);
            return ret;
        }
        
        /// <summary>    
        /// RSA公钥格式转换，java->.net    
        /// </summary>    
        /// <param name="publicKey">java生成的公钥</param>    
        /// <returns></returns>    
        public static string RSAPublicKeyJava2DotNet(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
        }
        
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="unsignStr">需要签名的字符串</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>MD5摘要</returns>
        public static string MD5Sign(string unsignStr, string inputCharset)
        {
            StringBuilder sb = new StringBuilder(32);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(inputCharset).GetBytes(unsignStr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }


        /// <summary>
        /// 签名
        /// </summary>
        /// <param name=" unsignStr ">需要签名的内容</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="keyPassword">私钥加密密码</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns></returns>
        public static string RSAPKCS12Sign(string unsignStr, string privateKeyParh, string keyPassword, string inputCharset)
        {
            RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
            crypt.FromXmlString(privateKeyParh);
            SHA1Managed sha1 = new SHA1Managed();
            Encoding code = Encoding.GetEncoding(inputCharset);
            byte[] data = code.GetBytes(unsignStr);
            byte[] hash = sha1.ComputeHash(data);
            byte[] signData = crypt.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
            return Convert.ToBase64String(signData);
        }

    }
}
