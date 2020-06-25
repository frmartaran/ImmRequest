using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.DataAccess.Resources;
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
            try
            {
                var toDelete = Get(id);
                Context.Topics.Remove(toDelete);
            }
            catch (ArgumentNullException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Delete");
                throw new DatabaseNotFoundException(message);
            }
        }

        public bool Exists(Topic topic)
        {
            return Context.Topics
                .Where(t => t.Id != topic.Id)
                .Any(t => t.Name == topic.Name);
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
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Topic Update(Topic objectToUpdate)
        {
            try
            {
                var topicToUpdate = Get(objectToUpdate.Id);
                topicToUpdate.Name = objectToUpdate.Name;
                topicToUpdate.Types = objectToUpdate.Types;
                topicToUpdate.Area = objectToUpdate.Area;
                return topicToUpdate;
            }
            catch (NullReferenceException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Update");
                throw new DatabaseNotFoundException(message);
            }
        }
    }
}