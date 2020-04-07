using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;

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
            return Context.Fields
                .Include(f => f.ParentType)
                .FirstOrDefault(f => f.Id == id);
        }

        public ICollection<BaseField> GetAll()
        {
            return Context.Fields
                .Include(f => f.ParentType)
                .ToList();
        }

        public void Insert(BaseField objectToAdd)
        {
            Context.Fields.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public BaseField Update(BaseField objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}