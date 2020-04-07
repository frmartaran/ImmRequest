using System;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.Fields;

namespace ImmRequest.DataAccess.Repositories
{
    public class FieldsRepository : IRepository<BaseField>
    {
        private ImmDbContext Context { get; set; }

        public FieldsRepository(ImmDbContext context)
        {
            Context = context;
        }
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public BaseField Get(long id)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.ICollection<BaseField> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(BaseField objectToAdd)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public BaseField Update(BaseField objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}