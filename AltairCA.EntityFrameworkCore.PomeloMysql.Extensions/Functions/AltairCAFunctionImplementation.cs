using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions
{
    internal class AltairCaFunctionImplementation:IMethodCallTranslator
    {
        public static string Password { get; set; }
        private readonly ISqlExpressionFactory _expressionFactory;

        private static readonly MethodInfo _encryptMethod
            = typeof(DbFunctionExtensions).GetMethod(
                nameof(DbFunctionExtensions.Encrypt),
                new[] { typeof(DbFunctions), typeof(string) });
        private static readonly MethodInfo _decryptMethod
            = typeof(DbFunctionExtensions).GetMethod(
                nameof(DbFunctionExtensions.Decrypt),
                new[] { typeof(DbFunctions), typeof(string) });

        private static readonly MethodInfo _encryptStringMethod
            = typeof(DbFunctionExtensions).GetMethod(
                nameof(DbFunctionExtensions.MySqlEncrypt),
                new[] { typeof(string) });
        private static readonly MethodInfo _decryptStringMethod
           = typeof(DbFunctionExtensions).GetMethod(
               nameof(DbFunctionExtensions.MySqlDecrypt),
               new[] { typeof(string) });

        private static readonly MethodInfo _nonTranslatableEncryptMethod
            = typeof(DbFunctionExtensions).GetMethod(
                nameof(DbFunctionExtensions.MySqlEncrypt),
                new[] { typeof(string), typeof(string) });
        private static readonly MethodInfo __nonTranslatableDecryptMethod
           = typeof(DbFunctionExtensions).GetMethod(
               nameof(DbFunctionExtensions.MySqlDecrypt),
               new[] { typeof(string), typeof(string) });
        public AltairCaFunctionImplementation(ISqlExpressionFactory expressionFactory)
        {
            _expressionFactory = expressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments,IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            var password = _expressionFactory.Constant(Password);
            if (method == _nonTranslatableEncryptMethod || method == __nonTranslatableDecryptMethod)
            {
                throw new Exception("Dont use this this will not translate to SQL. Use Decrypt() or Encrypt()");
            }
            if (method == _encryptStringMethod)
            {
                var value = arguments[0];
                
                var aesToHexExpression = AESEncryptionBuild(password, value);

                return aesToHexExpression;
            }
            if (method == _encryptMethod)
            {
                var value = arguments[1];
                
                var aesToHexExpression = AESEncryptionBuild( password, value);

                return aesToHexExpression;
            }

            if (method == _decryptMethod)
            {
                var value = arguments[1];
                
                var aesDecryptExpression = AesDecryptExpression(password, value);
                return _expressionFactory.Convert(aesDecryptExpression, typeof(string));
            }
            if (method == _decryptStringMethod)
            {
                var value = arguments[0];

                var aesDecryptExpression = AesDecryptExpression(password, value);
                return _expressionFactory.Convert(aesDecryptExpression, typeof(string));
            }
            return null;
        }

        private SqlFunctionExpression AesDecryptExpression(SqlExpression password, SqlExpression value)
        {
            
            var unhexValueExpression = _expressionFactory.Function( "UNHEX", new List<SqlExpression>()
            {
                value
            },true,new List<bool>()
            {
                true
            }, typeof(string));
            var aesDecryptExpression = _expressionFactory.Function( "AES_DECRYPT",
                new List<SqlExpression>()
                {
                    unhexValueExpression,
                    password
                },true,new List<bool>()
                {
                    true,
                    false
                }, typeof(byte[]));
            return aesDecryptExpression;
        }

        protected virtual SqlFunctionExpression AESEncryptionBuild( SqlExpression password,
            SqlExpression value)
        {
            var aesEncryptionExpression = _expressionFactory.Function( "AES_ENCRYPT",
                new List<SqlExpression>()
                {
                    value,
                    password
                },true,new List<bool>
                {
                    true,
                    false
                }, typeof(byte[]));
            var aesToHexExpression = _expressionFactory.Function( "HEX", new List<SqlExpression>()
            {
                aesEncryptionExpression
            },true,new List<bool>()
            {
                true
            }, typeof(string));
            return aesToHexExpression;
        }
    }
}
