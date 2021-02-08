﻿// <auto-generated />
using System;
using CarRepairShopRP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRepairShopRP.Migrations
{
    [DbContext(typeof(RepairShopContext))]
    [Migration("20210202185244_changeOil")]
    partial class changeOil
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.HasSequence<int>("InvoiceNumber")
                .StartsAt(0L)
                .HasMax(99999L)
                .IsCyclic();

            modelBuilder.Entity("CarRepairShopRP.Areas.Identity.Data.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("CarRepairShopRP.Areas.Identity.Data.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Car", b =>
                {
                    b.Property<int>("CarID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BodyType")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("EngineCapacity")
                        .HasColumnType("int");

                    b.Property<int>("EngineFuel")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("oilChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("productionYear")
                        .HasColumnType("bigint");

                    b.HasKey("CarID");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IssuedToId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RepairID")
                        .HasColumnType("int");

                    b.Property<decimal>("Sum")
                        .HasColumnType("money");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.HasKey("InvoiceID");

                    b.HasIndex("IssuedById");

                    b.HasIndex("IssuedToId");

                    b.HasIndex("RepairID");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Repair", b =>
                {
                    b.Property<int>("RepairID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AssignedMechanicID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CarID")
                        .HasColumnType("int");

                    b.Property<bool>("ChangeOil")
                        .HasColumnType("bit");

                    b.Property<string>("ClientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("InvoiceIssued")
                        .HasColumnType("bit");

                    b.Property<string>("ProblemDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("RepairState")
                        .HasColumnType("int");

                    b.Property<decimal>("WorkPrice")
                        .HasColumnType("money");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime2");

                    b.HasKey("RepairID");

                    b.HasIndex("AssignedMechanicID");

                    b.HasIndex("CarID");

                    b.HasIndex("ClientID");

                    b.ToTable("Repair");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.ReplacedPart", b =>
                {
                    b.Property<int>("ReplacedPartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RepairID")
                        .HasColumnType("int");

                    b.HasKey("ReplacedPartID");

                    b.HasIndex("RepairID");

                    b.ToTable("ReplacedPart");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Visit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("AcceptedClient")
                        .HasColumnType("bit");

                    b.Property<bool>("AcceptedMechanic")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PlannedVisitDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VisitClientID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VisitMechanicID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VisitPurpose")
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.HasKey("ID");

                    b.HasIndex("VisitClientID");

                    b.HasIndex("VisitMechanicID");

                    b.ToTable("Visit");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CarRepairShopRP.Areas.Identity.Data.ApplicationUserRole", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Invoice", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", "IssuedBy")
                        .WithMany()
                        .HasForeignKey("IssuedById");

                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", "IssuedTo")
                        .WithMany()
                        .HasForeignKey("IssuedToId");

                    b.HasOne("CarRepairShopRP.Data.Repair", "Repair")
                        .WithMany()
                        .HasForeignKey("RepairID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IssuedBy");

                    b.Navigation("IssuedTo");

                    b.Navigation("Repair");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Repair", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", "AssignedMechanic")
                        .WithMany("AssignedRepairs")
                        .HasForeignKey("AssignedMechanicID");

                    b.HasOne("CarRepairShopRP.Data.Car", "Car")
                        .WithMany("Repairs")
                        .HasForeignKey("CarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", "Client")
                        .WithMany("Repairs")
                        .HasForeignKey("ClientID");

                    b.Navigation("AssignedMechanic");

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.ReplacedPart", b =>
                {
                    b.HasOne("CarRepairShopRP.Data.Repair", "Repair")
                        .WithMany("ReplacedParts")
                        .HasForeignKey("RepairID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repair");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Visit", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", "VisitClient")
                        .WithMany("Visits")
                        .HasForeignKey("VisitClientID");

                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", "VisitMechanic")
                        .WithMany("AssignedVisits")
                        .HasForeignKey("VisitMechanicID");

                    b.Navigation("VisitClient");

                    b.Navigation("VisitMechanic");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRepairShopRP.Areas.Identity.Data.ApplicationRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CarRepairShopRP.Areas.Identity.Data.RepairShopUser", b =>
                {
                    b.Navigation("AssignedRepairs");

                    b.Navigation("AssignedVisits");

                    b.Navigation("Repairs");

                    b.Navigation("UserRoles");

                    b.Navigation("Visits");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Car", b =>
                {
                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("CarRepairShopRP.Data.Repair", b =>
                {
                    b.Navigation("ReplacedParts");
                });
#pragma warning restore 612, 618
        }
    }
}
