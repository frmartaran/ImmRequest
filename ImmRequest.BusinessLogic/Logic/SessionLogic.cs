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
    public class SessionLogic : ISessionLogic
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
            try
            {
                Repository.Delete(id);
            }
            catch (DatabaseNotFoundException exception)
            {
                LogicHelpers.WarnIfNotFound(exception, BusinessResource.Action_Delete,
                    Entity_Name);
            }
        }

        public Session Get(long Id)
        {
            var session = Repository.Get(Id);
            LogicHelpers.WarnIfNotFound(session, BusinessResource.Action_Get, Entity_Name);
            return session;
        }

        public ICollection<Session> GetAll()
        {
            return Repository.GetAll();
        }

        public void Update(Session objectToUpdate)
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
                LogicHelpers.WarnIfNotFound(exception, BusinessResource.Action_Update, Entity_Name);
            }
        }

        public bool IsValidToken(Guid token)
        {
            return Repository.GetAll().Any(s => s.Token == token);
        }
    }
}
