using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute
{
    internal class AttributeValueConvertor:ValueConverter<string, string>
    {
        private static string _password;
        private AttributeValueConvertor() : base(convertToProviderExpression, convertFromProviderExpression)
        {
            
        }

        public static AttributeValueConvertor GetInstance(string password)
        {
            _password = password;
            return new AttributeValueConvertor();
        }

        private static Expression<Func<string, string>> convertToProviderExpression = x => x.MySqlEncrypt(_password);

        private static Expression<Func<string, string>>
            convertFromProviderExpression = x => x.MySqlEncrypt(_password);
    }
}
