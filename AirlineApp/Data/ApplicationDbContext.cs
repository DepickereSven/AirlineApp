﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Models;
using AirlineApp.Entities;

namespace AirlineApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<FlightData> FlightData { get; set; }
        public virtual DbSet<Locatie> Locatie { get; set; }
        public virtual DbSet<Opgericht> Opgericht { get; set; }
        public virtual DbSet<Airlinecodes> Airlinecodes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Airlinecodes>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("airlinecodes");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

           builder.Entity<FlightData>(entity =>
            {
                entity.HasKey(e => e.FlightId);

                entity.Property(e => e.FlightId).HasColumnName("FlightID");

                entity.Property(e => e.AirlineCode)
                    .IsRequired()
                    .HasColumnName("Airline_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalAirport)
                    .IsRequired()
                    .HasColumnName("Arrival_Airport")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalLatitude).HasColumnName("Arrival_Latitude");

                entity.Property(e => e.ArrivalLongitude).HasColumnName("Arrival_Longitude");

                entity.Property(e => e.ArrivalState)
                    .IsRequired()
                    .HasColumnName("Arrival_State")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DepartureLatitude).HasColumnName("Departure_Latitude");

                entity.Property(e => e.DepatureAirport)
                    .IsRequired()
                    .HasColumnName("Depature_Airport")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DepatureLongitude).HasColumnName("Depature_Longitude");

                entity.Property(e => e.DepatureState)
                    .IsRequired()
                    .HasColumnName("Depature_State")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.AirlineCodeNavigation)
                    .WithMany(p => p.FlightData)
                    .HasForeignKey(d => d.AirlineCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightData_airlinecodes");
            });

            builder.Entity<Locatie>(entity =>
            {
                entity.HasKey(e => e.AirlineCode);

                entity.ToTable("locatie");

                entity.Property(e => e.AirlineCode)
                    .HasColumnName("airlineCode")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MainHub)
                    .IsRequired()
                    .HasColumnName("mainHub")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StaatHoofkwartier)
                    .IsRequired()
                    .HasColumnName("staatHoofkwartier")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StaatMainHub)
                    .IsRequired()
                    .HasColumnName("staatMainHub")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StadHoofkwartier)
                    .IsRequired()
                    .HasColumnName("stadHoofkwartier")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.AirlineCodeNavigation)
                    .WithOne(p => p.Locatie)
                    .HasForeignKey<Locatie>(d => d.AirlineCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_locatie_airlinecodes");
            });

            builder.Entity<Opgericht>(entity =>
            {
                entity.HasKey(e => e.AirlineCode);

                entity.Property(e => e.AirlineCode)
                    .HasColumnName("airlineCode")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Gestopt)
                    .HasColumnName("gestopt")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Opgericht1)
                    .IsRequired()
                    .HasColumnName("opgericht")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.AirlineCodeNavigation)
                    .WithOne(p => p.Opgericht)
                    .HasForeignKey<Opgericht>(d => d.AirlineCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opgericht_airlinecodes");
            });
        }
    }
}
