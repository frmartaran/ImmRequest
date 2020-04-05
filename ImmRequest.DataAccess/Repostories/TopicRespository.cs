using System;
using System.Collections.Generic;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;

namespace ImmRequest.DataAccess.Repositories
{
    public class TopicRepository : IRepository<Topic>
    {

        private ImmDbContext Context { get; set; }

        public TopicRepository(ImmDbContext context)
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

        public Topic Get(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Topic> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Topic objectToAdd)
        {
            Context.Topics.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Topic Update(Topic objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}