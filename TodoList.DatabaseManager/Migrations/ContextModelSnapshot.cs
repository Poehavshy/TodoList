﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TodoList.DatabaseManager.Managers.Postgresql;

#nullable disable

namespace TodoList.DatabaseManager.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<int>("TodoId")
                        .HasColumnType("integer")
                        .HasColumnName("todo_id");

                    b.HasKey("Id")
                        .HasName("pk_comments");

                    b.HasIndex("TodoId")
                        .HasDatabaseName("ix_comments_todo_id");

                    b.ToTable("comments", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Text = "comment 1",
                            TodoId = 1
                        },
                        new
                        {
                            Id = 2,
                            Text = "comment 2",
                            TodoId = 1
                        },
                        new
                        {
                            Id = 3,
                            Text = "comment 3",
                            TodoId = 1
                        });
                });

            modelBuilder.Entity("Models.Entities.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean")
                        .HasColumnName("is_done");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_todos");

                    b.HasIndex("Title", "Category")
                        .IsUnique()
                        .HasDatabaseName("ix_todos_title_category");

                    b.ToTable("todos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Analytics",
                            Color = "Red",
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDone = false,
                            Title = "Create a ticket"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Bookkeeping",
                            Color = "Green",
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDone = false,
                            Title = "Request information"
                        });
                });

            modelBuilder.Entity("Models.Entities.Comment", b =>
                {
                    b.HasOne("Models.Entities.Todo", null)
                        .WithMany("Comments")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_todos_todo_id");
                });

            modelBuilder.Entity("Models.Entities.Todo", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}