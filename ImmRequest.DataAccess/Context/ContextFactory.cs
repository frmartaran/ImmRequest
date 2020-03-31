using System;
using Microsoft.EntityFrameworkCore;

namespace ImmRequest.DataAccess.Context
{
    public class ContextFactory
    {

        public static ImmDbContext GetMemoryContext(string name)
        {
            var builder = new DbContextOptionsBuilder<ImmDbContext>();
            var options = GetMemoryConfig(name, builder);
            return new ImmDbContext(options);
        }

        private static DbContextOptions<ImmDbContext> GetMemoryConfig(string name,
        DbContextOptionsBuilder<ImmDbContext> builder)
        {
            builder.UseInMemoryDatabase(name);
            return builder.Options;
        }
    }
}
