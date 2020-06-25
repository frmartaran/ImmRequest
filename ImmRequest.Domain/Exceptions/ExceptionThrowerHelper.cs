using ImmRequest.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Domain.Exceptions
{
    public class ExceptionThrowerHelper
    {
        public static void ThrowMultipleSelectionDisable(string fieldName)
        {
            var message = string.Format(DomainResource.MultipleSelecction_Disable, fieldName);
            throw new DomainValidationException(message);
        }
    }
}
