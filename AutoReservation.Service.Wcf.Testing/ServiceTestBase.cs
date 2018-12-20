using System;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Xunit;
using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.BusinessLayer.Exceptions;

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
			List<AutoDto> resultList = Target.GetAllCars();
			for (int i = 0; i < list.Count; i++){
				Assert.True(list[i].ConvertToDto().CompareTo(resultList[i]));
			}
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
			List<KundeDto> resultList = Target.GetAllCustomers();
			for (int i = 0; i < list.Count; i++)
			{
				Assert.True(list[i].ConvertToDto().CompareTo(resultList[i]));
			}
			Assert.Equal(resultList.Count, list.Count);
		}

        [Fact]
        public void GetReservationenTest()
        {
			List<ReservationDto> list = new List<ReservationDto>
			{
				new ReservationDto { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
				new ReservationDto { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
				new ReservationDto { ReservationsNr = 3, AutoId = 3, KundeId = 3, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
				new ReservationDto { ReservationsNr = 4, AutoId = 2, KundeId = 1, Von = new DateTime(2020, 05, 19), Bis = new DateTime(2020, 06, 19)},
			};
			List<ReservationDto> resultList = Target.GetAllReservations();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Auto = Target.GetCar(list[i].AutoId);
				list[i].Kunde = Target.GetCustomer(list[i].KundeId);
				Assert.True(list[i].CompareTo(resultList[i]));
			}
			Assert.Equal(resultList.Count, list.Count);
		}

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
			AutoDto auto = new LuxusklasseAuto { Id = 3, Marke = "Audi S6", Tagestarif = 180, Basistarif = 50 }.ConvertToDto();
			AutoDto resultAuto = Target.GetCar(auto.Id);
			Assert.True(resultAuto.CompareTo(auto));
        }

        [Fact]
        public void GetKundeByIdTest()
        {
			KundeDto kunde = new KundeDto { Id = 3, Nachname = "Pfahl", Vorname = "Martha", Geburtsdatum = new DateTime(1990, 07, 03) };
			KundeDto resultKunde = Target.GetCustomer(kunde.Id);
			Assert.True(resultKunde.CompareTo(kunde));
        }

        [Fact]
        public void GetReservationByNrTest()
        {
			ReservationDto reservation = new ReservationDto { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20) };
			ReservationDto resultReservation = Target.GetReservation(reservation.ReservationsNr);
			reservation.Auto = Target.GetCar(reservation.AutoId);
			reservation.Kunde = Target.GetCustomer(reservation.KundeId);
			Assert.True(resultReservation.CompareTo(reservation));
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
			//TODO: Doesn't work sometimes
			AutoDto auto = (new LuxusklasseAuto {Marke = "Audi C4", Tagestarif = 1337, Basistarif = 101 }).ConvertToDto();
			Target.AddCar(auto);
			auto.Id = 5;
			AutoDto resultAuto = Target.GetCar(auto.Id);
			Assert.True(auto.CompareTo(resultAuto));
		}

        [Fact]
        public void InsertKundeTest()
        {
			KundeDto kunde = new KundeDto {Nachname = "Grey", Vorname = "Kuma", Geburtsdatum = new DateTime(666, 06, 06) };
			Target.AddCustomer(kunde);
			kunde.Id = 5;
			KundeDto resultKunde = Target.GetCustomer(kunde.Id);
			Assert.True(resultKunde.CompareTo(kunde));
		}

        [Fact]
        public void InsertReservationTest()
        {
			ReservationDto reservation = new ReservationDto {AutoId = 4, KundeId = 4, Von = new DateTime(2020, 01, 10), Bis = new DateTime(3020, 01, 20) };
			reservation.Auto = Target.GetCar(reservation.AutoId);
			reservation.Kunde = Target.GetCustomer(reservation.KundeId);
			Target.AddReservation(reservation);
			ReservationDto resultReservation = Target.GetReservation(5);
			Assert.True(resultReservation.CompareTo(reservation));
		}

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
			Assert.True(Target.DeleteCar(Target.GetCar(1)));
		}

        [Fact]
        public void DeleteKundeTest()
        {
			Assert.True(Target.DeleteCustomer(Target.GetCustomer(1)));
        }

        [Fact]
        public void DeleteReservationTest()
        {
			Assert.True(Target.DeleteReservation(Target.GetReservation(1)));
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
			AutoDto auto = Target.GetCar(2);
			auto.Basistarif = 666;
            Assert.True(Target.UpdateCar(auto));
        }

        [Fact]
        public void UpdateKundeTest()
        {
			KundeDto kunde = Target.GetCustomer(2);
			kunde.Vorname = "Bob";
			Assert.True(Target.UpdateCustomer(kunde));
        }

        [Fact]
        public void UpdateReservationTest()
        {
			ReservationDto reservation = Target.GetReservation(2);
			reservation.Kunde = Target.GetCustomer(2);
			Assert.True(Target.UpdateReservation(reservation));
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
			AutoDto auto = Target.GetCar(3);
			auto.Basistarif = 666;
			Target.UpdateCar(auto);
			auto.Tagestarif = 666;
			Assert.Throws<OptimisticConcurrencyException<Auto>>(() => Target.UpdateCar(auto));
		}

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
			KundeDto kunde = Target.GetCustomer(3);
			kunde.Vorname = "Bob";
			Target.UpdateCustomer(kunde);
			kunde.Nachname = "Bobbington";
			Assert.Throws<OptimisticConcurrencyException<Kunde>>(() => Target.UpdateCustomer(kunde));
		}

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
			ReservationDto reservation = Target.GetReservation(3);
			reservation.Kunde = Target.GetCustomer(3);
			Target.UpdateReservation(reservation);
			reservation.Auto = Target.GetCar(3);
			Assert.Throws<OptimisticConcurrencyException<Reservation>>(() => Target.UpdateReservation(reservation));
		}

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
			ReservationDto reservation = new ReservationDto { AutoId = 4, KundeId = 4, Von = new DateTime(2020, 01, 10), Bis = new DateTime(1020, 01, 20) };
			reservation.Auto = Target.GetCar(reservation.AutoId);
			reservation.Kunde = Target.GetCustomer(reservation.KundeId);
			Assert.Throws<InvalidDateRangeException>(() => Target.AddReservation(reservation));
		}

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
			ReservationDto reservation = new ReservationDto { ReservationsNr = 3, AutoId = 3, KundeId = 3, Von = new DateTime(2020, 01, 12), Bis = new DateTime(2020, 01, 18) };
			reservation.Auto = Target.GetCar(reservation.AutoId);
			reservation.Kunde = Target.GetCustomer(reservation.KundeId);
			Assert.Throws<AutoUnavailableException>(() => Target.AddReservation(reservation));
		}

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
			ReservationDto reservation = Target.GetReservation(3);
			reservation.Von = reservation.Bis.AddDays(1);
			reservation.Auto = Target.GetCar(reservation.AutoId);
			reservation.Kunde = Target.GetCustomer(reservation.KundeId);
			Assert.Throws<InvalidDateRangeException>(() => Target.UpdateReservation(reservation));
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
