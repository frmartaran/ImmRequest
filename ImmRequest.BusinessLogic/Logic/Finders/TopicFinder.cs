using ImmRequest.BusinessLogic.Helpers;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var topic = Repository.GetAll()
                .Where(t => condition.Invoke(t))
                .FirstOrDefault();
            LogicHelpers.WarnIfNotFound(topic, BusinessResource.Action_Find,
                BusinessResource.Entity_Topic);
            return topic;
        }

        public ICollection<Topic> FindAll()
        {
            return Repository.GetAll();
        }

        public ICollection<Topic> FindAll(Predicate<Topic> condition)
        {
            throw new NotImplementedException();
        }
    }
}
