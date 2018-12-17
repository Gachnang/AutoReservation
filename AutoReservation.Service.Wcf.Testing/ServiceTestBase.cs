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
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
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
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
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
