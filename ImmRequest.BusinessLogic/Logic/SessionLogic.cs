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
        public Guid Create(Session session)
        {
            session.Token = Guid.NewGuid();
            if (Validator.IsValid(session))
            {
                Repository.Insert(session);
            }
            return session.Token;
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

        public Session Get(Guid token)
        {
            var session = Repository.GetAll()
                .Where(s => s.Token == token)
                .FirstOrDefault();
            LogicHelpers.WarnIfNotFound(session, BusinessResource.Action_Get, Entity_Name);
            return session;
        }

        public bool IsValidToken(Guid token)
        {
            return Repository.GetAll().Any(s => s.Token == token);
        }
        public void Save()
        {
            Repository.Save();
        }
    }
}
