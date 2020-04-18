using System;
using System.Collections.Generic;
using System.Text;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MysqlEncryptAttribute:System.Attribute
    {
    }
}
