using System;
using System.Collections.Generic;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions
{
    internal class AltairCaMySqlMethodCallTranslatorPlugin:IMethodCallTranslatorPlugin
    {
        public AltairCaMySqlMethodCallTranslatorPlugin(IRelationalTypeMappingSource typeMappingSource,
            ISqlExpressionFactory sqlExpressionFactory) 
            
        {
            if (!(sqlExpressionFactory is MySqlSqlExpressionFactory npgsqlSqlExpressionFactory))
            {
                throw new ArgumentException($"Must be an {nameof(MySqlSqlExpressionFactory)}", nameof(sqlExpressionFactory));
            }
            Translators = new IMethodCallTranslator[] { new AltairCaFunctionImplementation(sqlExpressionFactory), };
        }

        public IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
