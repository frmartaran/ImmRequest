using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Helpers;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic.Finders
{
    public class Finder : IFinder
    {
        private IDatabaseFinder DBFinder { get; set; }

        public Finder(IDatabaseFinder finder)
        {
            DBFinder = finder;
        }

        public ICollection<T> FindAll<T>() where T : class
        {
            try
            {
                return DBFinder.FindAll<T>();
            }
            catch (DBDeveloperException exception)
            {
                throw new DeveloperException(BusinessResource.Developer_Exception, exception);
            }
        }

        public ICollection<T> FindAll<T>(Predicate<T> condition) where T : class
        {
            try
            {
                return DBFinder.FindAll<T>(condition);
            }
            catch (DBDeveloperException exception)
            {
                throw new DeveloperException(BusinessResource.Developer_Exception, exception);
            }
        }

        public T Find<T>(Predicate<T> condition) where T : class
        {
            try
            {
                var objectFound = DBFinder.Find<T>(condition);
                LogicHelpers.WarnIfNotFound(objectFound, BusinessResource.Action_Find, "");
                return objectFound;
            }
            catch (DBDeveloperException exception)
            {
                throw new DeveloperException(BusinessResource.Developer_Exception, exception);
            }
        }
    }
}
