using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EncryptionTest.Data
{
    public class ApplicationDbContext:DbContext
    {
        public readonly ILoggerFactory MyLoggerFactory;
        public DbSet<Testings> Testings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseEncryptionFunctions("NBJ42RKQ2vQoYFZOj1C83921vHExVhVp1PlOAK6gjbMZI");
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseEncryptAttribute("NBJ42RKQ2vQoYFZOj1C83921vHExVhVp1PlOAK6gjbMZI");
        }
    }
}