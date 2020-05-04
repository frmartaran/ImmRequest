﻿// <auto-generated />
using System;
using ImmRequest.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImmRequest.DataAccess.Migrations
{
    [DbContext(typeof(ImmDbContext))]
    [Migration("20200504013930_ChangedDeleteBehaviour")]
    partial class ChangedDeleteBehaviour
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImmRequest.Domain.Area", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ImmRequest.Domain.CitizenRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AreaId")
                        .HasColumnType("bigint");

                    b.Property<string>("CitizenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RequestNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("TopicId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("TopicId");

                    b.HasIndex("TopicTypeId");

                    b.ToTable("CitizenRequests");
                });

            modelBuilder.Entity("ImmRequest.Domain.Fields.BaseField", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ParentTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentTypeId");

                    b.ToTable("Fields");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseField");
                });

            modelBuilder.Entity("ImmRequest.Domain.RequestFieldValues", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("FieldId")
                        .HasColumnType("bigint");

                    b.Property<long>("ParentCitizenRequestId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCitizenRequestId");

                    b.ToTable("RequestFieldValues");
                });

            modelBuilder.Entity("ImmRequest.Domain.Topic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AreaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("ImmRequest.Domain.TopicType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ParentTopicId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentTopicId");

                    b.ToTable("TopicTypes");
                });

            modelBuilder.Entity("ImmRequest.Domain.UserManagement.Administrator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("ImmRequest.Domain.UserManagement.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AdministratorId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Token")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ImmRequest.Domain.Fields.DateTimeField", b =>
                {
                    b.HasBaseType("ImmRequest.Domain.Fields.BaseField");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("DateTimeField");
                });

            modelBuilder.Entity("ImmRequest.Domain.Fields.NumberField", b =>
                {
                    b.HasBaseType("ImmRequest.Domain.Fields.BaseField");

                    b.Property<int>("RangeEnd")
                        .HasColumnType("int");

                    b.Property<int>("RangeStart")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("NumberField");
                });

            modelBuilder.Entity("ImmRequest.Domain.Fields.TextField", b =>
                {
                    b.HasBaseType("ImmRequest.Domain.Fields.BaseField");

                    b.Property<string>("RangeValues")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("TextField");
                });

            modelBuilder.Entity("ImmRequest.Domain.CitizenRequest", b =>
                {
                    b.HasOne("ImmRequest.Domain.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ImmRequest.Domain.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ImmRequest.Domain.TopicType", "TopicType")
                        .WithMany()
                        .HasForeignKey("TopicTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImmRequest.Domain.Fields.BaseField", b =>
                {
                    b.HasOne("ImmRequest.Domain.TopicType", "ParentType")
                        .WithMany("AllFields")
                        .HasForeignKey("ParentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImmRequest.Domain.RequestFieldValues", b =>
                {
                    b.HasOne("ImmRequest.Domain.CitizenRequest", "ParentCitizenRequest")
                        .WithMany("Values")
                        .HasForeignKey("ParentCitizenRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImmRequest.Domain.Topic", b =>
                {
                    b.HasOne("ImmRequest.Domain.Area", "Area")
                        .WithMany("Topics")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImmRequest.Domain.TopicType", b =>
                {
                    b.HasOne("ImmRequest.Domain.Topic", "ParentTopic")
                        .WithMany("Types")
                        .HasForeignKey("ParentTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImmRequest.Domain.UserManagement.Session", b =>
                {
                    b.HasOne("ImmRequest.Domain.UserManagement.Administrator", "AdministratorInSession")
                        .WithMany()
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
