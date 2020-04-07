using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Exceptions;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Resources;
using ImmRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;

namespace ImmRequest.DataAccess.Repositories
{
    public class FieldsRepository : IRepository<BaseField>
    {
        private ImmDbContext Context { get; set; }

        public FieldsRepository(ImmDbContext context)
        {
            Context = context;
        }
        public void Delete(long id)
        {
            try
            {
                var fieldToDelete = Get(1);
                Context.Fields.Remove(fieldToDelete);
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
            return Context.Fields.Any(f => f.Id == id);
        }

        public BaseField Get(long id)
        {
            return Context.Fields
                .Include(f => f.ParentType)
                .FirstOrDefault(f => f.Id == id);
        }

        public ICollection<BaseField> GetAll()
        {
            return Context.Fields
                .Include(f => f.ParentType)
                .ToList();
        }

        public void Insert(BaseField objectToAdd)
        {
            Context.Fields.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public BaseField Update(BaseField objectToUpdate)
        {
            var fieldToModify = Get(objectToUpdate.Id);
            fieldToModify.UpdateValues(objectToUpdate);
            Save();
            return fieldToModify;
        }
    }
}