﻿// <auto-generated />
using System;
using BookStore.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BookStore.Infrastructure.Data.Migrations.EventStoreSql
{
    [DbContext(typeof(EventStoreSqlContext))]
    [Migration("20210726100415_InitialSchema")]
    partial class InitialSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BookStore.Domain.Core.Events.StoredEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uuid")
                        .HasColumnName("aggregate_id");

                    b.Property<string>("Data")
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<string>("MessageType")
                        .HasColumnType("text")
                        .HasColumnName("message_type");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("timestamp");

                    b.Property<string>("User")
                        .HasColumnType("text")
                        .HasColumnName("user");

                    b.HasKey("Id")
                        .HasName("pk_stored_events");

                    b.ToTable("stored_events");
                });
#pragma warning restore 612, 618
        }
    }
}