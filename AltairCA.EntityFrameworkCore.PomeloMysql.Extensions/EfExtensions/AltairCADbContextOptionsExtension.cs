using System;
using System.Collections.Generic;
using System.Text;
using AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.Attribute;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace AltairCA.EntityFrameworkCore.PomeloMysql.Extensions.EfExtensions
{
    internal class AltairCADbContextOptionsExtension:IDbContextOptionsExtension
    {
        private DbContextOptionsExtensionInfo _info;
        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<IMethodCallTranslatorProvider, AltairCaMySqlMethodCallTranslatorPlugin>();
            services.AddSingleton<IRelationalTypeMappingSourcePlugin, EncryptAttributeTypeMappingPlugin>();
        }

        public void Validate(IDbContextOptions options)
        {
            
        }

        public DbContextOptionsExtensionInfo Info
        {
            get
            {
                return this._info ?? (MyDbContextOptionsExtensionInfo)new MyDbContextOptionsExtensionInfo((IDbContextOptionsExtension)this);
            }
        }
        private sealed class MyDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
        {
            public MyDbContextOptionsExtensionInfo(IDbContextOptionsExtension instance) : base(instance) { }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => "";

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {

            }

            public override long GetServiceProviderHashCode()
            {
                return 0;
            }
        }
    }
}
