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
    public class AreaFinder : IFinder<Area>
    {
        private IRepository<Area> Repository { get; set; }

        public AreaFinder(IRepository<Area> repository)
        {
            Repository = repository;
        }

        public Area Find(Predicate<Area> condition)
        {
            var area = Repository.GetAll()
                .Where(a => condition.Invoke(a))
                .FirstOrDefault();
            LogicHelpers.WarnIfNotFound(area, BusinessResource.Action_Find,
                BusinessResource.Entity_Area);
            return area;
        }

        public ICollection<Area> FindAll()
        {
            return Repository.GetAll();
        }

        public ICollection<Area> FindAll(Predicate<Area> condition)
        {
            return Repository.GetAll()
                .Where(a => condition.Invoke(a))
                .ToList();
        }
    }
}
