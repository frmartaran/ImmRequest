using ImmRequest.BusinessLogic.Helpers;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic
{
    public class TopicTypeLogic : ILogic<TopicType>
    {
        private IRepository<TopicType> Repository { get; set; }

        private IValidator<TopicType> Validator { get; set; }

        public TopicTypeLogic(IRepository<TopicType> repository, IValidator<TopicType> validator)
        {
            Repository = repository;
            Validator = validator;
        }
        public void Create(TopicType objectToCreate)
        {
            if (Validator.IsValid(objectToCreate))
            {
                objectToCreate.ParentTopic.Types.Add(objectToCreate);
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
                LogicHelpers.WarnIfNotFound(exception,
                    BusinessResource.Action_Delete, BusinessResource.Entity_TopicType);
            }
        }

        public TopicType Get(long Id)
        {
            var type = Repository.Get(Id);
            LogicHelpers.WarnIfNotFound(type,
                BusinessResource.Action_Get, BusinessResource.Entity_TopicType);
            return type;
        }

        public ICollection<TopicType> GetAll()
        {
            return Repository.GetAll();
        }

        public void Update(TopicType objectToUpdate)
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
                    BusinessResource.Entity_TopicType);
            }

        }
    }
}
