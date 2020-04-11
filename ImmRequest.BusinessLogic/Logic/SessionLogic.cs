using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
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

        private const string Entity_Name = "Session";

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
            Repository.Delete(id);
        }

        public Session Get(long Id)
        {
            var session = Repository.Get(Id);
            WarnIfNotFound(session, BusinessResource.Action_Get);
            return session;
        }

        private void WarnIfNotFound(Session session, string action)
        {
            if (session == null)
            {
                var message = string.Format(BusinessResource.LogicAction_NotFound,
                    action, Entity_Name);
                throw new BusinessLogicException(message);
            }
        }

        public ICollection<Session> GetAll()
        {
            return Repository.GetAll();
        }

        public void Update(Session objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
