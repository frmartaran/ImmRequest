using System;
using System.Collections.Generic;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;

namespace ImmRequest.DataAccess.Repositories
{
    public class CitizenRequestRepository : IRepository<CitizenRequest>
    {
        private ImmDbContext Context { get; set; }
        public CitizenRequestRepository(ImmDbContext context)
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

        public CitizenRequest Get(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<CitizenRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(CitizenRequest objectToAdd)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public CitizenRequest Update(CitizenRequest objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}