using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Helpers
{
    public class LogicHelpers
    {
        public static void WarnIfNotFound(DatabaseNotFoundException exception, string action, 
            string entity)
        {
            var message = string.Format(BusinessResource.LogicAction_NotFound,
                                action, entity);
            throw new BusinessLogicException(message, exception);
        }

        public static void WarnIfNotFound<T>(T entity, string action, string entityName)
        {
            if (entity == null)
            {
                var message = string.Format(BusinessResource.LogicAction_NotFound,
                    action, entityName);
                throw new BusinessLogicException(message);
            }
        }
    }
}
