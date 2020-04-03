using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using Microsoft.EntityFrameworkCore;

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
            return Context.CitizenRequests
                .Include(cr => cr.Area)
                .FirstOrDefault(cr => cr.Id == id);
        }

        public ICollection<CitizenRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(CitizenRequest objectToAdd)
        {
            Context.CitizenRequests.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
           Context.SaveChanges();
        }

        public CitizenRequest Update(CitizenRequest objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}