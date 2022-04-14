using System;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute;

namespace EncryptionTest.Data
{
    public class Testings
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MysqlEncrypt]
        public string? encrypted { get; set; }
        public string? normal { get; set; }
    }
}