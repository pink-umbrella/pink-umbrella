﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PinkUmbrella.Repositories;

namespace PinkUmbrella.Migrations.LogDb
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20200614205821_Group access and exception log")]
    partial class Groupaccessandexceptionlog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("PinkUmbrella.Models.LoggedExceptionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Logged")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Serialized")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("StackTrace")
                        .HasColumnType("TEXT");

                    b.Property<string>("Stringified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Exceptions");
                });
#pragma warning restore 612, 618
        }
    }
}