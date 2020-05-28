using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Validators
{
    public class AreaValidator : IValidator<Area>
    {
        private IRepository<Area> Repository { get; set; }

        public AreaValidator(IRepository<Area> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Area objectToValidate)
        {
            return true;
        }
    }
}
