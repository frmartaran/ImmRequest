using System;
using System.Collections.Generic;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Interfaces;
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

        public virtual DbSet<BaseField> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TextField>()
                        .Property(e => e.RangeValues)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<List<string>>(v));
            builder.Entity<NumberField>()
            .Property(nf => nf.RangeStart);

            builder.Entity<NumberField>()
            .Property(nf => nf.RangeEnd);

            builder.Entity<DateTimeField>()
            .Property(nf => nf.Start);

            builder.Entity<DateTimeField>()
            .Property(nf => nf.End);


            builder.Entity<CitizenRequest>()
                .HasOne(cr => cr.Area)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CitizenRequest>()
                .HasOne(cr => cr.Topic)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RequestFieldValues>()
                .HasOne(rf => rf.Field)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<TopicType>()
                .Property<bool>("IsDeleted");
            builder.Entity<TopicType>()
                .HasQueryFilter(ty => !ty.IsDeleted);

            builder.Entity<BaseField>()
                .Property<bool>("IsDeleted");
            builder.Entity<BaseField>()
                .HasQueryFilter(ty => !ty.IsDeleted);
        }

        public override int SaveChanges()
        {
            SoftDelete();
            return base.SaveChanges();
        }

        private void SoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entry.Entity.GetType()))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["IsDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.CurrentValues["DeletedDate"] = DateTime.Now;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }
            }
        }
    }
}
