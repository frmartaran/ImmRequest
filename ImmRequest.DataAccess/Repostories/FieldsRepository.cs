using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
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

            }
            catch (ArgumentNullException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Delete");
                throw new DatabaseNotFoundException(message);
            }
        }

        public bool Exists(BaseField field)
        {
            return Context.Fields.Any(f => f.Id == field.Id);
        }

        public BaseField Get(long id)
        {
            return Context.Fields
                .IgnoreQueryFilters()
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
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public BaseField Update(BaseField objectToUpdate)
        {
            try
            {
                var fieldToModify = Get(objectToUpdate.Id);
                fieldToModify.UpdateValues(objectToUpdate);
                return fieldToModify;

            }
            catch (NullReferenceException)
            {
                var message = string.Format(DataAccessResource.Exception_NotFound_Action, "Update");
                throw new DatabaseNotFoundException(message);
            }
        }
    }
}