using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImmRequest.DataAccess.Repositories
{
    public class AreaRepository : IRepository<Area>
    {
        private ImmDbContext Context { get; set; }

        public AreaRepository(ImmDbContext context)
        {
            Context = context;
        }
        public void Delete(long id)
        {
            var areaToDelete = Get(id);
            Context.Areas.Remove(areaToDelete);
            Save();
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public Area Get(long id)
        {
            return Context.Areas
                .Include(a => a.Topics)
                    .ThenInclude(t => t.Types)
                    .ThenInclude(ty => ty.AllFields)
                .FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Area> GetAll()
        {
            return Context.Areas
                .Include(a => a.Topics)
                    .ThenInclude(t => t.Types)
                    .ThenInclude(ty => ty.AllFields)
                    .ToList();
        }

        public void Insert(Area objectToAdd)
        {
            Context.Areas.Add(objectToAdd);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Area Update(Area objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}