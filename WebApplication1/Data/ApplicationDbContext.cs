using System;
using System.Collections.Generic;
using System.Text;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TestModel> TModels { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseEncryptionFunctions();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseEncryptAttribute("HelloWorld");
        }
    }

    public class TestModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        //[MysqlEncrypt]
        public string Name { get; set; }
    }
}
