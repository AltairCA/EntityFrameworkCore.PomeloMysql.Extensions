using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute
{
    public static class AttributeEncryptExtension
    {
        public static string MySqlEncrypt(this string value, string password)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return AESCipherMysql.AES_encrypt(value, password);
        }
        public static string MySqlDecrypt(this string value, string password)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return AESCipherMysql.AES_decrypt(value, password);
        }
    }
}