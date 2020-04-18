using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute
{
    public class AttributeValueRelationalMapping: RelationalTypeMapping
    {
        private static readonly ValueConverter<string, string> _converter
      = new ValueConverter<string, string>(color => color.MySqlEncrypt("HelloWorld"),
                                          name => name.MySqlEncrypt("HelloWorld"));

        public AttributeValueRelationalMapping() :
            base(new RelationalTypeMappingParameters(
                new CoreTypeMappingParameters(typeof(string),_converter),"longtext" 
            ))
        {

        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new AttributeValueRelationalMapping();
        }
    }

    public class EncryptAttributeTypeMappingPlugin : IRelationalTypeMappingSourcePlugin
    {
        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            if (mappingInfo.ClrType == typeof(string))
            {
                var attributes = mappingInfo.ClrType.GetCustomAttributes(typeof(MysqlEncryptAttribute), true);
                if (attributes.Any())
                {
                    return new AttributeValueRelationalMapping();
                }
            }
            
            return null;
        }
        public class Test
        {

        }
    }
    public class DemoCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
        {
            return base.CommandCreating(eventData, result);
        }
    }
}
