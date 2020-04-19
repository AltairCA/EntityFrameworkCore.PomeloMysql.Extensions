using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AltairCA.Utils.Extensions;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute
{
    public static class AESCipherMysql
    {
        public static String AES_encrypt(String Input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = mkey(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);
                    cs.Write(xXml, 0, xXml.Length);
                    cs.FlushFinalBlock();
                }

                xBuff = ms.ToArray();
            }
            
            return xBuff.ToHexString();
        }
        public static String AES_decrypt(String Input, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = mkey(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decrypt = aes.CreateDecryptor();
            byte[] encryptedStr = Input.FromHex2ByteArray();

            string Plain_Text;
            
            using (var ms = new MemoryStream(encryptedStr))
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        Plain_Text = reader.ReadToEnd();
                    }
                }
            }
            
            return Plain_Text;
        }
        private static byte[] mkey(string skey)
        {
            
            byte[] key = Encoding.UTF8.GetBytes(skey);
            byte[] k = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < key.Length; i++)
            {
                k[i % 16] = (byte)(k[i % 16] ^ key[i]);
            }

            return k;
        }
    }
}