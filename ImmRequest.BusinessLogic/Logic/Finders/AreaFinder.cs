using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic.Finders
{
    public class AreaFinder : IFinder<Area>
    {
        private IRepository<Area> Repository { get; set; }

        public AreaFinder(IRepository<Area> repository)
        {
            Repository = repository;
        }
        public Area Find(long id)
        {
            return Repository.Get(id);
        }

        public Area Find(Predicate<bool> condition)
        {
            throw new NotImplementedException();
        }

        public List<Area> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
