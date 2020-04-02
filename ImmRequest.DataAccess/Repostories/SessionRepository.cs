using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public Session Get(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Session> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Session objectToAdd)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Session Update(Session objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
