using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Exceptions;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Resources;
using ImmRequest.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.DataAccess.Repostories
{
    public class SessionRepository : IRepository<Session>
    {
        private ImmDbContext Context { get; set; }

        public SessionRepository(ImmDbContext context)
        {
            Context = context;
        }
        public void Delete(long id)
        {
            try
            {
                var sessionToDelete = Get(id);
                Context.Sessions.Remove(sessionToDelete);
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

        public Session Get(long id)
        {
            return Context.Sessions
                .Include(s => s.AdministratorInSession)
                .FirstOrDefault(s => s.Id == id);
        }

        public ICollection<Session> GetAll()
        {
            return Context.Sessions
                .Include(s => s.AdministratorInSession)
                .ToList();
        }

        public void Insert(Session objectToAdd)
        {
            Context.Sessions.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Session Update(Session objectToUpdate)
        {
            try
            {
                var sessionToModify = Get(objectToUpdate.Id);
                sessionToModify.AdministratorInSession = objectToUpdate.AdministratorInSession;
                sessionToModify.AdministratorId = objectToUpdate.Id;
                sessionToModify.Token = objectToUpdate.Token;
                Save();
                return sessionToModify;
            }
            catch (NullReferenceException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Update");
                throw new DatabaseNotFoundException(message);
            }
        }
    }
}
