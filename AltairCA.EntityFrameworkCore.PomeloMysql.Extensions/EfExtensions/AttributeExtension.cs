using System;
using System.Linq;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions
{
    public static class AttributeExtension
    {
		private static readonly ValueConverter<string, string> _converter
	 = new ValueConverter<string, string>(color => color.MySqlEncrypt(Password),
										 name => name.MySqlDecrypt(Password));
		private static string Password { get; set; }
		public static ModelBuilder UseEncryptAttribute(this ModelBuilder builder,string password)
        {

            AttributeExtension.Password = password.ToSha512();
            foreach (var entityType in builder.Model.GetEntityTypes())
			{
				foreach (var property in entityType.GetProperties())
				{
					var attributes = property.PropertyInfo.GetCustomAttributes(typeof(MysqlEncryptAttribute), false);
					if (attributes.Any())
					{
						//property.SetProviderClrType(typeof(EncString));
						property.SetValueConverter(_converter);
						//property.SetValueConverter(AttributeValueConvertor.GetInstance(password));
					}
				}
			}
			return builder;
        }
    }
}
