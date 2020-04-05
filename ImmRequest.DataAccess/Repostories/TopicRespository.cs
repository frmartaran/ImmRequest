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
            var toDelete = Get(id);
            Context.Topics.Remove(toDelete);
            Save();
        }

        public bool Exists(long id)
        {
            return Context.Topics.Any(t => t.Id == id);
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
            return Context.Topics
                .Include(t => t.Area)
                .Include(t => t.Types)
                    .ThenInclude(ty => ty.AllFields)
                .ToList();
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