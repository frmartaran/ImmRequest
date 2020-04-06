using System;
using System.Collections.Generic;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;

namespace ImmRequest.DataAccess.Repositories
{
    public class TopicTypeRepository : IRepository<TopicType>
    {
        private ImmDbContext Context { get; set; }

        public TopicTypeRepository(ImmDbContext context)
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

        public TopicType Get(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<TopicType> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(TopicType objectToAdd)
        {
            Context.TopicTypes.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public TopicType Update(TopicType objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}