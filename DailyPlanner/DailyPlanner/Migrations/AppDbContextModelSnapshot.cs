﻿// <auto-generated />
using System;
using DailyPlanner.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DailyPlanner.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.DailyTaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DailyTasksListId")
                        .HasColumnType("int");

                    b.Property<int>("Importance")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TaskDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DailyTasksListId");

                    b.ToTable("DailyTasks");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.DailyTasksListEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int>("UserEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("DailyTasksLists");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.GeneralTaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("Date");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("Date");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TaskDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("GeneralTasks");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.DailyTaskEntity", b =>
                {
                    b.HasOne("DailyPlanner.Repository.Entitites.DailyTasksListEntity", "DailyTasksList")
                        .WithMany("DailyTasks")
                        .HasForeignKey("DailyTasksListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DailyTasksList");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.DailyTasksListEntity", b =>
                {
                    b.HasOne("DailyPlanner.Repository.Entitites.UserEntity", "UserEntity")
                        .WithMany("DailyTasksLists")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.GeneralTaskEntity", b =>
                {
                    b.HasOne("DailyPlanner.Repository.Entitites.UserEntity", "UserEntity")
                        .WithMany("GeneralTasks")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.DailyTasksListEntity", b =>
                {
                    b.Navigation("DailyTasks");
                });

            modelBuilder.Entity("DailyPlanner.Repository.Entitites.UserEntity", b =>
                {
                    b.Navigation("DailyTasksLists");

                    b.Navigation("GeneralTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
