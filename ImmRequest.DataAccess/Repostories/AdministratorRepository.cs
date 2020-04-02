using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
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
            var toRemove = Get(id);
            Context.Administrators.Remove(toRemove);
            Save();
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
            var administratorToModify = Get(objectToUpdate.Id);
            administratorToModify.UserName = objectToUpdate.UserName;
            administratorToModify.PassWord = objectToUpdate.PassWord;
            Save();
            return administratorToModify;
        }
    }
}