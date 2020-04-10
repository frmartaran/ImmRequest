using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic
{
    public class AdministratorLogic : ILogic<Administrator>
    {
        private IRepository<Administrator> Repository { get; set; }
        private IValidator<Administrator> Validator { get; set; }

        public AdministratorLogic(IRepository<Administrator> repository, 
            IValidator<Administrator> validator)
        {
            Repository = repository;
            Validator = validator;
        }
        public void Create(Administrator objectToCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Administrator Get(int Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Administrator> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Administrator objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
