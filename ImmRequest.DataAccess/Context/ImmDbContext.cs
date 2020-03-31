using System;
using System.Collections.Generic;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ImmRequest.DataAccess.Context
{
    public class ImmDbContext : DbContext
    {
        public ImmDbContext(DbContextOptions<ImmDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Administrator> Administrators { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public virtual DbSet<CitizenRequest> CitizenRequests { get; set; }

        public virtual DbSet<Area> Areas { get; set; }

        public virtual DbSet<Topic> Topics { get; set; }

        public virtual DbSet<TopicType> TopicTypes { get; set; }

        public virtual DbSet<NumberField> NumberFields { get; set; }

        public virtual DbSet<TextField> TextFields { get; set; }

        public virtual DbSet<DateTimeField> DateTimeFields { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TextField>()
                        .Property(e => e.RangeValues)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<List<string>>(v));

        }
    }
}
