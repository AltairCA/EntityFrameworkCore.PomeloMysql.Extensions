using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute
{
    public static class AttributeExtension
    {
        public static ModelBuilder UseEncryptAttribute(this ModelBuilder builder,string password)
        {
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				foreach (var property in entityType.GetProperties())
				{
					var attributes = property.PropertyInfo.GetCustomAttributes(typeof(MysqlEncryptAttribute), false);
					if (attributes.Any())
					{
						property.SetValueConverter(AttributeValueConvertor.GetInstance(password));
					}
				}
			}
			return builder;
        }
    }
}
