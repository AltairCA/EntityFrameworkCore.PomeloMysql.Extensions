using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Functions;
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
            foreach (var property in mappingInfo.ClrType.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(MysqlEncryptAttribute), false);
                if (attributes.Any())
                {
                    return new AttributeValueRelationalMapping();
                }
            }
            return null;
        }
    }
}
