using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoReservation.Dal {
    public class AutoReservationContext : DbContext {

        public virtual DbSet<Auto> Autos { get; set; }
        public virtual DbSet<Kunde> Kunden { get; set; }
        public virtual DbSet<Reservation> Reservationen { get; set; }
        
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(
            new[] { new ConsoleLoggerProvider((_, logLevel) => logLevel >= LogLevel.Information, true) }
        );

        public AutoReservationContext() {
            if (this.Database.EnsureCreated()) {
                List<Auto> autos = new List<Auto>() {
                    new Auto() {
                        Marke = "Fiat Punto", AutoKlasse = AutoKlasse.Standard, Tagestarif = 50, Basistarif = 0
                    },
                    new Auto() {
                        Marke = "VW Golf", AutoKlasse = AutoKlasse.Mittelklasse, Tagestarif = 120, Basistarif = 0
                    },
                    new Auto() {
                        Marke = "Audi S6", AutoKlasse = AutoKlasse.Luxusklasse, Tagestarif = 180, Basistarif = 50
                    }
                };


                List<Kunde> kunden = new List<Kunde>() {
                    new Kunde() { Nachname = "Nass", Vorname = "Anna", Geburtsdatum = Convert.ToDateTime("1961-05-05 00:00:00")},
                    new Kunde() { Nachname = "Beil", Vorname = "Timo", Geburtsdatum = Convert.ToDateTime("1980-09-09 00:00:00")},
                    new Kunde() { Nachname = "Pfahl", Vorname = "Marta", Geburtsdatum = Convert.ToDateTime("1950-07-03 00:00:00")},
                    new Kunde() { Nachname = "Zufall", Vorname = "Rainer", Geburtsdatum = Convert.ToDateTime("1944-11-11 00:00:00")}
                };

                this.Autos.AddRange(autos);
                this.Kunden.AddRange(kunden);

                this.Reservationen.AddRange(new List<Reservation>() {
                    new Reservation() {
                        Auto = autos[0],
                        Kunde = kunden[0],
                        Von = Convert.ToDateTime("2018-05-05 00:00:00"),
                        Bis = Convert.ToDateTime("2018-10-15 00:00:00")
                    }
                });

                this.SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured) {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
