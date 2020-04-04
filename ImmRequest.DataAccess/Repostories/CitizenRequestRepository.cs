using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Exceptions;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Resources;
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
            try
            {
                var toDelete = Get(id);
                Context.CitizenRequests.Remove(toDelete);
                Save();
            }
            catch (ArgumentNullException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Delete");
                throw new DatabaseNotFoundException(message);
            }
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public CitizenRequest Get(long id)
        {
            return Context.CitizenRequests
                .Include(cr => cr.Area)
                .Include(cr => cr.Topic)
                .Include(cr => cr.TopicType)
                .Include(cr => cr.Values)
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