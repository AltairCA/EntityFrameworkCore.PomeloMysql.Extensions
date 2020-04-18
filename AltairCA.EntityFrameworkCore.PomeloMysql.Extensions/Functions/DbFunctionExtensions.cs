using System;
using Microsoft.EntityFrameworkCore;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions
{
    public static class DbFunctionExtensions
    {
        public static string Encrypt(this DbFunctions _,string value, string password)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core Mysql only.");
        }

        public static string Decrypt(this DbFunctions _,string value, string password )
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core Mysql only.");
        }
        public static string MySqlEncrypt(this string value, string password)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core Mysql only.");
        }
    }
}
