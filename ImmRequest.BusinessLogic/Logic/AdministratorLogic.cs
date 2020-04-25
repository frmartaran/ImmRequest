using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Helpers;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic
{
    public class AdministratorLogic : IAdministratorLogic
    {
        private IRepository<Administrator> Repository { get; set; }
        private IValidator<Administrator> Validator { get; set; }

        private const string Entity_Name = "Administrator";

        public AdministratorLogic(IRepository<Administrator> repository,
            IValidator<Administrator> validator)
        {
            Repository = repository;
            Validator = validator;
        }
        public void Create(Administrator objectToCreate)
        {
            if (Validator.IsValid(objectToCreate))
            {
                Repository.Insert(objectToCreate);
            }
        }

        public void Delete(long id)
        {
            try
            {
                Repository.Delete(id);
            }
            catch (DatabaseNotFoundException exception)
            {
                LogicHelpers.WarnIfNotFound(exception, BusinessResource.Action_Delete,
                    Entity_Name);
                return;
            }
        }



        public Administrator Get(long Id)
        {
            var administrator = Repository.Get(Id);
            LogicHelpers.WarnIfNotFound(administrator,
                BusinessResource.Action_Get, Entity_Name);
            return administrator;
        }


        public ICollection<Administrator> GetAll()
        {
            return Repository.GetAll();
        }

        public void Update(Administrator objectToUpdate)
        {
            try
            {
                if (Validator.IsValid(objectToUpdate))
                {
                    Repository.Update(objectToUpdate);
                }

            }
            catch (DatabaseNotFoundException exception)
            {
                LogicHelpers.WarnIfNotFound(exception, BusinessResource.Action_Update,
                    Entity_Name);
            }
        }

        public Administrator FindAdministratorByCredentials(string email, string password)
        {
            return Repository.GetAll()
                .Where(a => a.Email == email)
                .Where(a => a.PassWord == password)
                .FirstOrDefault();
        }
    }
}
