using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using Microsoft.EntityFrameworkCore;

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
            return Context.Topics
                .Include(t => t.Area)
                .Include(t => t.Types)
                    .ThenInclude(ty => ty.AllFields)
                .FirstOrDefault(t => t.Id == id);
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