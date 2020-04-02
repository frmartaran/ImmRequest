using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
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
            var sessionToDelete = Get(id);
            Context.Sessions.Remove(sessionToDelete);
            Save();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
