﻿// <auto-generated />
using System;
using AzamAfridi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AzamAfridi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AzamAfridi.Models.ExpenseOnRoute", b =>
                {
                    b.Property<int>("ExpenseOnRouteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseOnRouteID"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("int");

                    b.Property<string>("RouteDetailBuiltyNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RouteDetailRouteID")
                        .HasColumnType("int");

                    b.Property<int>("RouteID")
                        .HasColumnType("int");

                    b.HasKey("ExpenseOnRouteID");

                    b.HasIndex("ExpenseTypeId");

                    b.HasIndex("RouteDetailRouteID", "RouteDetailBuiltyNo");

                    b.ToTable("ExpenseOnRoutes");
                });

            modelBuilder.Entity("AzamAfridi.Models.ExpenseType", b =>
                {
                    b.Property<int>("ExpenseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseTypeId"));

                    b.Property<string>("ExpenseTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseTypeId");

                    b.ToTable("ExpenseTypes");

                    b.HasData(
                        new
                        {
                            ExpenseTypeId = 1,
                            ExpenseTypeCode = "Diesel-Lit",
                            ExpenseTypeDescription = "Diesel Litter"
                        },
                        new
                        {
                            ExpenseTypeId = 2,
                            ExpenseTypeCode = "Fix-Chrg",
                            ExpenseTypeDescription = "Fixed Charges"
                        },
                        new
                        {
                            ExpenseTypeId = 3,
                            ExpenseTypeCode = "TollTax",
                            ExpenseTypeDescription = "Toll Tax"
                        });
                });

            modelBuilder.Entity("AzamAfridi.Models.RouteDetail", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteID"));

                    b.Property<string>("BuiltyNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DriveName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FromFare")
                        .HasColumnType("float");

                    b.Property<string>("FromStation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Return_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Return_FromStation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Return_ToStation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Return_Weight")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("ToFare")
                        .HasColumnType("float");

                    b.Property<string>("ToStation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalExpense")
                        .HasColumnType("float");

                    b.Property<double>("TotalFare")
                        .HasColumnType("float");

                    b.Property<double>("TotalIncome")
                        .HasColumnType("float");

                    b.Property<string>("TruckNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("RouteID", "BuiltyNo");

                    b.ToTable("RouteDetails");
                });

            modelBuilder.Entity("AzamAfridi.Models.StationName", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StationId"));

                    b.Property<string>("StationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StationId");

                    b.ToTable("StationNames");

                    b.HasData(
                        new
                        {
                            StationId = 1,
                            StationCode = "LHR",
                            StationDescription = "Lahore"
                        },
                        new
                        {
                            StationId = 2,
                            StationCode = "KHI",
                            StationDescription = "Karachi"
                        },
                        new
                        {
                            StationId = 3,
                            StationCode = "GUJ",
                            StationDescription = "Gujrawala"
                        },
                        new
                        {
                            StationId = 4,
                            StationCode = "MLT",
                            StationDescription = "Multan"
                        },
                        new
                        {
                            StationId = 5,
                            StationCode = "PESH",
                            StationDescription = "Peshawar"
                        },
                        new
                        {
                            StationId = 6,
                            StationCode = "MUR",
                            StationDescription = "Murree"
                        },
                        new
                        {
                            StationId = 7,
                            StationCode = "KHT",
                            StationDescription = "Kohat"
                        },
                        new
                        {
                            StationId = 8,
                            StationCode = "SHK",
                            StationDescription = "Sheikhapura"
                        });
                });

            modelBuilder.Entity("AzamAfridi.Models.ExpenseOnRoute", b =>
                {
                    b.HasOne("AzamAfridi.Models.ExpenseType", "ExpenseType")
                        .WithMany()
                        .HasForeignKey("ExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzamAfridi.Models.RouteDetail", "RouteDetail")
                        .WithMany("Expenses")
                        .HasForeignKey("RouteDetailRouteID", "RouteDetailBuiltyNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpenseType");

                    b.Navigation("RouteDetail");
                });

            modelBuilder.Entity("AzamAfridi.Models.RouteDetail", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
