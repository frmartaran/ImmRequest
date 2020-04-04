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
            return Context.CitizenRequests.Any(cr => cr.Id == id);
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
            return Context.CitizenRequests
                .Include(cr => cr.Area)
                .Include(cr => cr.Topic)
                .Include(cr => cr.TopicType)
                .Include(cr => cr.Values)
                .ToList();
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
            try
            {
                var requestToModify = Get(objectToUpdate.Id);
                requestToModify.Description = objectToUpdate.Description;
                requestToModify.CitizenName = objectToUpdate.CitizenName;
                requestToModify.Email = objectToUpdate.Email;
                requestToModify.Phone = objectToUpdate.Phone;
                requestToModify.RequestNumber = objectToUpdate.RequestNumber;
                requestToModify.Status = objectToUpdate.Status;
                requestToModify.Area = objectToUpdate.Area;
                requestToModify.Topic = objectToUpdate.Topic;
                requestToModify.TopicType = objectToUpdate.TopicType;
                requestToModify.Values = objectToUpdate.Values;
                Save();
                return requestToModify;
            }
            catch (NullReferenceException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Update");
                throw new DatabaseNotFoundException(message);
            }
        }
    }
}