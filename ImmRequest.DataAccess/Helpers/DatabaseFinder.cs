using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.DataAccess.Helpers
{
    public class DatabaseFinder : IDatabaseFinder
    {
        private ImmDbContext context;

        public DatabaseFinder(ImmDbContext context)
        {
            this.context = context;
        }
        public T Find<T>(Predicate<T> condition) where T : class
        {
            var objectFound = context.Set<T>()
                .ToList()
                .Where(x => condition.Invoke(x))
                .FirstOrDefault();
            return objectFound;
        }

        public ICollection<T> FindAll<T>(Predicate<T> condition)
        {
            throw new NotImplementedException();
        }
    }
}
