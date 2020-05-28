using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Validators
{
    public class TopicValidator : IValidator<Topic>
    {
        private IRepository<Topic> Repository { get; set; }

        public TopicValidator(IRepository<Topic> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Topic objectToValidate)
        {
            return true;
        }
    }
}
