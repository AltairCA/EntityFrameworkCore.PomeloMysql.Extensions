using System;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Extensions
{
    internal static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}
