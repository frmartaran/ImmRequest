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
            try
            {
                var topicToUpdate = Get(objectToUpdate.Id);
                topicToUpdate.Name = objectToUpdate.Name;
                topicToUpdate.Types = objectToUpdate.Types;
                topicToUpdate.Area = objectToUpdate.Area;
                Save();
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