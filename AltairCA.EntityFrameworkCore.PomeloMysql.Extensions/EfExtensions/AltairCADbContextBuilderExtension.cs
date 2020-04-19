using System;
using System.Collections.Generic;
using System.Text;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions
{
    public static class AltairCADbContextBuilderExtension
    {
        public static DbContextOptionsBuilder UseEncryptionFunctions(
            this DbContextOptionsBuilder optionsBuilder,string password)
        {
            var extension = (AltairCADbContextOptionsExtension)GetOrCreateExtension(optionsBuilder,password);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
            return optionsBuilder;
        }

        private static AltairCADbContextOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder,string password)
            => optionsBuilder.Options.FindExtension<AltairCADbContextOptionsExtension>()
               ?? new AltairCADbContextOptionsExtension(password);
    }
}
