using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic.Finders
{
    public class TopicFinder : IFinder<Topic>
    {
        private IRepository<Topic> Repository { get; set; }

        public TopicFinder(IRepository<Topic> repository)
        {
            Repository = repository;
        }
        public Topic Find(Predicate<Topic> condition)
        {
            throw new NotImplementedException();
        }

        public ICollection<Topic> FindAll()
        {
            throw new NotImplementedException();
        }

        public ICollection<Topic> FindAll(Predicate<Topic> condition)
        {
            throw new NotImplementedException();
        }
    }
}
