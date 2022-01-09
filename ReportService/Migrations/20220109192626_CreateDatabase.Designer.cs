﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReportService.Data;

namespace ReportService.Migrations
{
    [DbContext(typeof(ReportContext))]
    [Migration("20220109192626_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ReportService.Models.ReportRequest", b =>
                {
                    b.Property<string>("UUID")
                        .HasColumnType("text");

                    b.Property<string>("ReportFileURL")
                        .HasColumnType("text");

                    b.Property<string>("ReportState")
                        .HasColumnType("text");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("UUID");

                    b.ToTable("ReportRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
