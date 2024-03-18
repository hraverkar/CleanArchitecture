﻿// <auto-generated />
using System;
using CleanArchitecture.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchitecture.Migrations.Migrations
{
    [DbContext(typeof(WeatherContext))]
    partial class WeatherContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitecture.Core.CarCompanies.Entities.CarCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CarManufactureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarCompanies");
                });

            modelBuilder.Entity("CleanArchitecture.Core.Locations.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("CleanArchitecture.Core.Locations.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("CleanArchitecture.Core.Weather.Entities.RegisterUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RegisterUsers");
                });

            modelBuilder.Entity("CleanArchitecture.Core.Weather.Entities.WeatherForecast", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("CleanArchitecture.Core.Locations.Entities.Location", b =>
                {
                    b.OwnsOne("CleanArchitecture.Core.Locations.ValueObjects.Coordinates", "Coordinates", b1 =>
                        {
                            b1.Property<Guid>("LocationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Latitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Latitude");

                            b1.Property<decimal>("Longitude")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Longitude");

                            b1.HasKey("LocationId");

                            b1.ToTable("Locations");

                            b1.WithOwner()
                                .HasForeignKey("LocationId");
                        });

                    b.Navigation("Coordinates")
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitecture.Core.Weather.Entities.WeatherForecast", b =>
                {
                    b.HasOne("CleanArchitecture.Core.Locations.Entities.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CleanArchitecture.Core.Weather.ValueObjects.Temperature", "Temperature", b1 =>
                        {
                            b1.Property<Guid>("WeatherForecastId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Celcius")
                                .HasColumnType("int")
                                .HasColumnName("Temperature");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.Navigation("Temperature")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
