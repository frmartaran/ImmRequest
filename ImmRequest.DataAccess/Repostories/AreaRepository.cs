using System;
using System.Collections.Generic;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;

namespace ImmRequest.DataAccess.Repositories
{
    public class AreaRepository : IRepository<Area>
    {
        private ImmDbContext Context { get; set; }

        public AreaRepository(ImmDbContext context)
        {
            Context = context;
        }
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public Area Get(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Area> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Area objectToAdd)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Area Update(Area objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}