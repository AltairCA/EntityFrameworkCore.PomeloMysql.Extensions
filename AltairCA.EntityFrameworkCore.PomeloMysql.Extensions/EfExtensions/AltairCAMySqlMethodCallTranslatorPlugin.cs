using System.Collections.Generic;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;
using Microsoft.EntityFrameworkCore.Query;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions
{
    internal class AltairCaMySqlMethodCallTranslatorPlugin:MySqlMethodCallTranslatorProvider
    {
        public AltairCaMySqlMethodCallTranslatorPlugin(RelationalMethodCallTranslatorProviderDependencies dependencies) : base(dependencies)
        {
            ISqlExpressionFactory expressionFactory = dependencies.SqlExpressionFactory;
            this.AddTranslators(new List<IMethodCallTranslator>
            {
                new AltairCaFunctionImplementation(expressionFactory)
            });
        }
    }
}
