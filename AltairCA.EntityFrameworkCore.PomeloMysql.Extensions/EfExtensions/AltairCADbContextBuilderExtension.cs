using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions
{
    public static class AltairCADbContextBuilderExtension
    {
        public static DbContextOptionsBuilder UseEncryptionFunctions(
            this DbContextOptionsBuilder optionsBuilder)
        {
            var extension = (AltairCADbContextOptionsExtension)GetOrCreateExtension(optionsBuilder);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }

        private static AltairCADbContextOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<AltairCADbContextOptionsExtension>()
               ?? new AltairCADbContextOptionsExtension();
    }
}
