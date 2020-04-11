﻿using ImmRequest.BusinessLogic.Exceptions;
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

        private const string Entity_Name = "Administrator";
        private const string Delete_Action = "Deleting";

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
                var message = string.Format(BusinessResource.LogicAction_NotFound, 
                    Delete_Action, Entity_Name);
                throw new BusinessLogicException(message, exception);
            }
        }

        public Administrator Get(long Id)
        {
            return Repository.Get(Id);
        }

        public ICollection<Administrator> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Administrator objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
