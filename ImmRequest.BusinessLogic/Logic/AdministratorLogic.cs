using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic
{
    public class AdministratorLogic : ILogic<Administrator>
    {
        private IRepository<Administrator> Repository { get; set; }
        private IValidator<Administrator> Validator { get; set; }

        private const string Entity_Name = "an Administrator";

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
                WarnIfNotFound(exception, BusinessResource.Action_Delete);
                return;
            }
        }

        private void WarnIfNotFound(DatabaseNotFoundException exception, string action)
        {
            var message = string.Format(BusinessResource.LogicAction_NotFound,
                                action, Entity_Name);
            throw new BusinessLogicException(message, exception);
        }

        public Administrator Get(long Id)
        {
            var administrator = Repository.Get(Id);
            WarnIfNotFound(administrator, BusinessResource.Action_Get);
            return administrator;
        }

        private void WarnIfNotFound(Administrator administrator, string action)
        {
            if (administrator == null)
            {
                var message = string.Format(BusinessResource.LogicAction_NotFound,
                    action, Entity_Name);
                throw new BusinessLogicException(message);
            }
        }

        public ICollection<Administrator> GetAll()
        {
            return Repository.GetAll();
        }

        public void Update(Administrator objectToUpdate)
        {
            if (Validator.IsValid(objectToUpdate))
            {
                Repository.Update(objectToUpdate);
            }
        }
    }
}
