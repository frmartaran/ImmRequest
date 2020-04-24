using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
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
                Repository.Insert(objectToCreate);
            }
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public TopicType Get(long Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<TopicType> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TopicType objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
