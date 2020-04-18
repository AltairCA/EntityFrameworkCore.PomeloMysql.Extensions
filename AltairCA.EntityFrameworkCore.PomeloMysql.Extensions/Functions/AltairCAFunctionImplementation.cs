using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions
{
    internal class AltairCaFunctionImplementation:IMethodCallTranslator
    {
        private readonly ISqlExpressionFactory _expressionFactory;

        private static readonly MethodInfo _encryptMethod
            = typeof(DbFunctionExtensions).GetMethod(
                nameof(DbFunctionExtensions.Encrypt),
                new[] { typeof(DbFunctions), typeof(string), typeof(string) });
        private static readonly MethodInfo _decryptMethod
            = typeof(DbFunctionExtensions).GetMethod(
                nameof(DbFunctionExtensions.Decrypt),
                new[] { typeof(DbFunctions), typeof(string), typeof(string) });

        private static readonly MethodInfo _encryptStringMethod
            = typeof(DbFunctionExtensions).GetMethod(
                nameof(DbFunctionExtensions.MySqlEncrypt),
                new[] { typeof(string), typeof(string) });
        public AltairCaFunctionImplementation(ISqlExpressionFactory expressionFactory)
        {
            _expressionFactory = expressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (method == _encryptStringMethod)
            {
                var value = arguments[0];
                var password = arguments[1];
                var sha2Expression = _expressionFactory.Function(instance, "SHA2", new List<SqlExpression>()
                {
                    password,
                    new SqlFragmentExpression("512")
                }, typeof(string));
                var shaUnhexExpress = _expressionFactory.Function(instance, "UNHEX", new List<SqlExpression>()
                {
                    sha2Expression
                }, typeof(string));
                var aesEncryptionExpression = _expressionFactory.Function(instance, "AES_ENCRYPT",
                    new List<SqlExpression>()
                    {
                        value,
                        shaUnhexExpress
                    }, typeof(byte[]));
                var aesToHexExpression = _expressionFactory.Function(instance, "HEX", new List<SqlExpression>()
                {
                    aesEncryptionExpression
                }, typeof(string));

                return aesToHexExpression;
            }
            if (method == _encryptMethod)
            {
                var value = arguments[1];
                var password = arguments[2];
                var sha2Expression = _expressionFactory.Function(instance, "SHA2", new List<SqlExpression>()
                {
                    password,
                    new SqlFragmentExpression("512")
                },typeof(string));
                var shaUnhexExpress = _expressionFactory.Function(instance, "UNHEX", new List<SqlExpression>()
                {
                    sha2Expression
                }, typeof(string));
                var aesEncryptionExpression = _expressionFactory.Function(instance, "AES_ENCRYPT",
                    new List<SqlExpression>()
                    {
                        value,
                        shaUnhexExpress
                    }, typeof(byte[]));
                var aesToHexExpression = _expressionFactory.Function(instance, "HEX", new List<SqlExpression>()
                {
                    aesEncryptionExpression
                }, typeof(string));

                return aesToHexExpression;
            }

            if (method == _decryptMethod)
            {
                var value = arguments[1];
                var password = arguments[2];
                var sha2Expression = _expressionFactory.Function(instance, "SHA2", new List<SqlExpression>()
                {
                    password,
                    new SqlFragmentExpression("512")
                }, typeof(string));
                var shaUnhexExpress = _expressionFactory.Function(instance, "UNHEX", new List<SqlExpression>()
                {
                    sha2Expression
                }, typeof(string));
                var unhexValueExpression = _expressionFactory.Function(instance, "UNHEX", new List<SqlExpression>()
                {
                    value
                }, typeof(string));
                var aesDecryptExpression = _expressionFactory.Function(instance, "AES_DECRYPT",
                    new List<SqlExpression>()
                    {
                        unhexValueExpression,
                        shaUnhexExpress
                    }, typeof(byte[]));
                return _expressionFactory.Convert(aesDecryptExpression, typeof(string));
            }
            return null;
        }
    }
}
