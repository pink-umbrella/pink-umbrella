﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PinkUmbrella.Repositories;

namespace PinkUmbrella.Migrations.AuthDb
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20200704150838_Add Key Challenge")]
    partial class AddKeyChallenge
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("PinkUmbrella.Models.Auth.AuthSitePermissionModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AuthId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OverrideValue")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Permission")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SitePermissions");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.FIDOCredential", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("AaGuid")
                        .HasColumnType("TEXT");

                    b.Property<string>("CredType")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PublicKey")
                        .HasColumnType("BLOB");

                    b.Property<uint>("SignatureCounter")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransportTypes")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("WhenCreated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FIDOCredentials");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.IPAddressModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<long>("PublicKeyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PublicKeyId");

                    b.ToTable("IPs");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.IPBlockModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ByUserId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IPId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("WhenBlocked")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("IPBlocks");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.KeyChallengeModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Challenge")
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<long>("KeyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("KeyChallenges");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.PrivateKey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Format")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PublicKeyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("WhenAdded")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PublicKeyId");

                    b.ToTable("PrivateKeys");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.PublicKey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FingerPrint")
                        .HasColumnType("TEXT");

                    b.Property<int>("Format")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("WhenAdded")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PublicKeys");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.UserAuthKeyModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("PublicKeyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PublicKeyId");

                    b.ToTable("UserAuthKeys");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.UserLoginMethodModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Method")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("WhenCreated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserLoginMethods");
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.IPAddressModel", b =>
                {
                    b.HasOne("PinkUmbrella.Models.Auth.PublicKey", "PublicKey")
                        .WithMany()
                        .HasForeignKey("PublicKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.PrivateKey", b =>
                {
                    b.HasOne("PinkUmbrella.Models.Auth.PublicKey", "PublicKey")
                        .WithMany()
                        .HasForeignKey("PublicKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PinkUmbrella.Models.Auth.UserAuthKeyModel", b =>
                {
                    b.HasOne("PinkUmbrella.Models.Auth.PublicKey", "PublicKey")
                        .WithMany()
                        .HasForeignKey("PublicKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
