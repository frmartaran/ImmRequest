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
    public class TopicTypeRepository : IRepository<TopicType>
    {
        private ImmDbContext Context { get; set; }

        public TopicTypeRepository(ImmDbContext context)
        {
            Context = context;
        }
        public void Delete(long id)
        {
            try
            {
                var typeToDelete = Get(id);
                Context.TopicTypes.Remove(typeToDelete);
            }
            catch (ArgumentNullException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Delete");
                throw new DatabaseNotFoundException(message);
            }
        }

        public bool Exists(TopicType type)
        {
            return Context.TopicTypes.Any(ty => ty.Id == type.Id);
        }

        public TopicType Get(long id)
        {
            return Context.TopicTypes
                .Include(ty => ty.ParentTopic)
                .Include(ty => ty.AllFields)
                .FirstOrDefault(ty => ty.Id == id);
        }

        public ICollection<TopicType> GetAll()
        {
            return Context.TopicTypes
                .Include(ty => ty.ParentTopic)
                .Include(ty => ty.AllFields)
                .ToList();
        }

        public void Insert(TopicType objectToAdd)
        {
            Context.TopicTypes.Add(objectToAdd);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public TopicType Update(TopicType objectToUpdate)
        {
            try
            {
                var typeToModify = Get(objectToUpdate.Id);
                typeToModify.Name = objectToUpdate.Name;
                typeToModify.ParentTopic = objectToUpdate.ParentTopic;
                typeToModify.AllFields = objectToUpdate.AllFields;
                return typeToModify;
            }
            catch (NullReferenceException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Update");
                throw new DatabaseNotFoundException(message);
            }

        }
    }
}