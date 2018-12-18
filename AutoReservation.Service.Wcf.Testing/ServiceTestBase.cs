using System;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Xunit;
using System.Collections.Generic;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase
    {
        protected abstract IAutoReservationService Target { get; }



        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
			List<Auto> list = new List<Auto>
			{
				new StandardAuto {Id = 1, Marke = "Fiat Punto", Tagestarif = 50},
				new MittelklasseAuto {Id = 2, Marke = "VW Golf", Tagestarif = 120},
				new LuxusklasseAuto {Id = 3, Marke = "Audi S6", Tagestarif = 180, Basistarif = 50},
				new StandardAuto {Id = 4, Marke = "Fiat 500", Tagestarif = 75},
			};
			List<Auto> resultList = Target.GetAllCars().ConvertToEntities();
			Assert.Equal(resultList.Count, list.Count);
		}

        [Fact]
        public void GetKundenTest()
        {
			List<Kunde> list = new List<Kunde>
			{
				new Kunde {Id = 1, Nachname = "Nass", Vorname = "Anna", Geburtsdatum = new DateTime(1981, 05, 05)},
				new Kunde {Id = 2, Nachname = "Beil", Vorname = "Timo", Geburtsdatum = new DateTime(1980, 09, 09)},
				new Kunde {Id = 3, Nachname = "Pfahl", Vorname = "Martha", Geburtsdatum = new DateTime(1990, 07, 03)},
				new Kunde {Id = 4, Nachname = "Zufall", Vorname = "Rainer", Geburtsdatum = new DateTime(1954, 11, 11)},
			};
			List<Kunde> resultList = Target.GetAllCustomers().ConvertToEntities();
			Assert.Equal(resultList.Count, list.Count);
		}

        [Fact]
        public void GetReservationenTest()
        {
			List<Reservation> list = new List<Reservation>
			{
				new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
				new Reservation { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
				new Reservation { ReservationsNr = 3, AutoId = 3, KundeId = 3, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
				new Reservation { ReservationsNr = 4, AutoId = 2, KundeId = 1, Von = new DateTime(2020, 05, 19), Bis = new DateTime(2020, 06, 19)},
			};
			List<Reservation> resultList = Target.GetAllReservations().ConvertToEntities();
			Assert.Equal(resultList.Count, list.Count);
		}

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
			LuxusklasseAuto auto = new LuxusklasseAuto { Id = 3, Marke = "Audi S6", Tagestarif = 180, Basistarif = 50 };
			LuxusklasseAuto resultAuto = (LuxusklasseAuto)Target.GetCar(auto.Id).ConvertToEntity();
			Assert.Equal(auto.AutoKlasseId, resultAuto.AutoKlasseId);
			Assert.Equal(auto.Marke, resultAuto.Marke);
			Assert.Equal(auto.Tagestarif, resultAuto.Tagestarif);
        }

        [Fact]
        public void GetKundeByIdTest()
        {
			Kunde kunde = new Kunde { Id = 3, Nachname = "Pfahl", Vorname = "Martha", Geburtsdatum = new DateTime(1990, 07, 03) };
			Kunde resultKunde = (Kunde)Target.GetCustomer(kunde.Id).ConvertToEntity();
			Assert.Equal(kunde.Vorname, resultKunde.Vorname);
			Assert.Equal(kunde.Nachname, resultKunde.Nachname);
			Assert.Equal(kunde.Geburtsdatum, resultKunde.Geburtsdatum);
        }

        [Fact]
        public void GetReservationByNrTest()
        {
			Reservation reservation = new Reservation { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20) };
			Reservation resultReservation = Target.GetReservation(reservation.ReservationsNr).ConvertToEntity();
			Assert.Equal(reservation.Kunde, resultReservation.Kunde);
			Assert.Equal(reservation.Auto, resultReservation.Auto);
		}

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
			//TODO: What do we do with an illegal ID? We're not throwing an exception.
			Assert.Throws<ArgumentException>(() => Target.GetCar(-1));
		}

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
			Assert.Throws<ArgumentException>(() => Target.GetCustomer(-1));
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
			LuxusklasseAuto auto = new LuxusklasseAuto {Marke = "Audi C4", Tagestarif = 1337, Basistarif = 101 };
			Target.AddCar(auto.ConvertToDto());
			LuxusklasseAuto resultAuto = (LuxusklasseAuto)Target.GetCar(5).ConvertToEntity();
			Assert.Equal(auto.AutoKlasseId, resultAuto.AutoKlasseId);
			Assert.Equal(auto.Marke, resultAuto.Marke);
			Assert.Equal(auto.Tagestarif, resultAuto.Tagestarif);
		}

        [Fact]
        public void InsertKundeTest()
        {
			Kunde kunde = new Kunde {Nachname = "Grey", Vorname = "Kuma", Geburtsdatum = new DateTime(666, 06, 06) };
			Target.AddCustomer(kunde.ConvertToDto());
			Kunde resultKunde = Target.GetCustomer(5).ConvertToEntity();
			Assert.Equal(kunde.Vorname, resultKunde.Vorname);
			Assert.Equal(kunde.Nachname, resultKunde.Nachname);
			Assert.Equal(kunde.Geburtsdatum, resultKunde.Geburtsdatum);
		}

        [Fact]
        public void InsertReservationTest()
        {
			//TODO: Throws Exception
			Reservation reservation = new Reservation {AutoId = 4, KundeId = 4, Von = new DateTime(2020, 01, 10), Bis = new DateTime(3020, 01, 20) };
			Target.AddReservation(reservation.ConvertToDto());			
			Reservation resultReservation = Target.GetReservation(5).ConvertToEntity();
			Assert.Equal(reservation.Kunde, resultReservation.Kunde);
			Assert.Equal(reservation.Auto, resultReservation.Auto);
		}

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
			LuxusklasseAuto auto = new LuxusklasseAuto { Marke = "Audi C5", Tagestarif = 1337, Basistarif = 101 };
			Target.AddCar(auto.ConvertToDto());
			Target.DeleteCar(auto.ConvertToDto());
			LuxusklasseAuto resultAuto = (LuxusklasseAuto)Target.GetCar(5).ConvertToEntity();
			Assert.Equal(auto.AutoKlasseId, resultAuto.AutoKlasseId);
			Assert.Equal(auto.Marke, resultAuto.Marke);
			Assert.Equal(auto.Tagestarif, resultAuto.Tagestarif);
		}

        [Fact]
        public void DeleteKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void DeleteReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion
    }
}
