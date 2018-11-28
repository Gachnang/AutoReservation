﻿using System.Configuration;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoReservation.Dal
{
    public class AutoReservationContext
        : DbContext
    {

        public DbSet<Auto> Autos { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Reservation> Reservationen { get; set; }


        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(
            new[] { new ConsoleLoggerProvider((_, logLevel) => logLevel >= LogLevel.Information, true) }
        );

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                    .UseSqlServer(ConfigurationManager.ConnectionStrings[nameof(AutoReservationContext)].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne<Auto>(reservation => reservation.Auto as Auto)
                .WithMany(auto => auto.Reservationen)
                .HasForeignKey(reservation => reservation.AutoId)
                .HasPrincipalKey(auto => auto.Id)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .HasOne<Kunde>(reservation => reservation.Kunde as Kunde)
                .WithMany(kunde => kunde.Reservationen)
                .HasForeignKey(reservation => reservation.KundeId)
                .HasPrincipalKey(kunde => kunde.Id)
                .IsRequired();
        }
    }
}