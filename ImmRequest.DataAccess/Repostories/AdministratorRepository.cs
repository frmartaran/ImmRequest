using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Exceptions;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Resources;
using ImmRequest.Domain.UserManagement;

namespace ImmRequest.DataAccess.Repositories
{
    public class AdministratorRepository : IRepository<Administrator>
    {
        private ImmDbContext Context { get; set; }
        public AdministratorRepository(ImmDbContext context)
        {
            Context = context;
        }
        public void Delete(long id)
        {
            try
            {
                var toRemove = Get(id);
                Context.Administrators.Remove(toRemove);
                Save();

            }
            catch (ArgumentNullException)
            {
                var exceptionMessage = string.Format(DataAccessResource.Exception_NotFound_Action,
                    "Delete");
                throw new DatabaseNotFoundException(exceptionMessage);
            }
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public Administrator Get(long id)
        {
            return Context.Administrators
                .FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Administrator> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Administrator objectToAdd)
        {
            Context.Administrators.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Administrator Update(Administrator objectToUpdate)
        {
            try
            {
                var administratorToModify = Get(objectToUpdate.Id);
                administratorToModify.UserName = objectToUpdate.UserName;
                administratorToModify.PassWord = objectToUpdate.PassWord;
                Save();
                return administratorToModify;
            }
            catch (NullReferenceException)
            {
                var exceptionMessage = string.Format(DataAccessResource.Exception_NotFound_Action,
                    "Update");
                throw new DatabaseNotFoundException(exceptionMessage);
            }

        }
    }
}