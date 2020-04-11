using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic
{
    public class SessionLogic : ILogic<Session>
    {
        private IRepository<Session> Repository { get; set; }
        private IValidator<Session> Validator { get; set; }

        public SessionLogic(IRepository<Session> repository, IValidator<Session> validator)
        {
            Repository = repository;
            Validator = validator;
        }
        public void Create(Session objectToCreate)
        {
            if (Validator.IsValid(objectToCreate))
            {
                Repository.Insert(objectToCreate);
            }
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Session Get(long Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Session> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Session objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
