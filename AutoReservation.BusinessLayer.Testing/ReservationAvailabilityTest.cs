using System;
using AutoReservation.TestEnvironment;
using Xunit;
using AutoReservation.Dal.Entities;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        public ReservationAvailabilityTest()
        {
            // Prepare reservation
            Reservation reservation = Target.GetReservation(1);
            reservation.Von = DateTime.Today;
            reservation.Bis = DateTime.Today.AddDays(1);
            Target.UpdateReservation(reservation);
        }

        [Fact]
        public void InsertingReservationTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Target.InsertReservation(reservation);
            int count = Target.ListOfReservationen.Count;
            Reservation reservationDB = Target.GetReservation(count);
            Assert.NotNull(reservationDB.Auto);
        }

        [Fact]
        public void DatesRightAfterTest()
        {
            Reservation reservation1 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Reservation reservation2 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 20),
                Bis = new DateTime(2020, 01, 30)
            };
            Target.InsertReservation(reservation1);
            Target.InsertReservation(reservation2);
            int count = Target.ListOfReservationen.Count;
            Reservation reservation1DB = Target.GetReservation(count - 1);
            Reservation reservation2DB = Target.GetReservation(count);
            Assert.Equal(reservation1DB.AutoId, reservation2DB.AutoId);
        }

        [Fact]
        public void DatesFarAfterTest()
        {
            Reservation reservation1 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Reservation reservation2 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 02, 10),
                Bis = new DateTime(2020, 02, 20)
            };
            Target.InsertReservation(reservation1);
            Target.InsertReservation(reservation2);
            int count = Target.ListOfReservationen.Count;
            Reservation reservation1DB = Target.GetReservation(count - 1);
            Reservation reservation2DB = Target.GetReservation(count);
            Assert.Equal(reservation1DB.AutoId, reservation2DB.AutoId);
        }

//        [Fact]
//        public void NotReallyOkayTest()
//        {
//            Assert.True(true);
//        }

        [Fact]
        public void SameDatesTest()
        {
            Reservation reservation1 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Reservation reservation2 = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Target.InsertReservation(reservation1);
            Assert.Throws<AutoUnavailableException>(() => Target.InsertReservation(reservation2));
        }

        [Fact]
        public void LeftOverlapTest()
        {
            Reservation reservation1 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Reservation reservation2 = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 08),
                Bis = new DateTime(2020, 01, 18)
            };
            Target.InsertReservation(reservation1);
            Assert.Throws<AutoUnavailableException>(() => Target.InsertReservation(reservation2));
        }

        [Fact]
        public void RightOverlapTest()
        {
            Reservation reservation1 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Reservation reservation2 = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 12),
                Bis = new DateTime(2020, 01, 22)
            };
            Target.InsertReservation(reservation1);
            Assert.Throws<AutoUnavailableException>(() => Target.InsertReservation(reservation2));
        }

        [Fact]
        public void ContainedTest()
        {
            Reservation reservation1 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Reservation reservation2 = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 12),
                Bis = new DateTime(2020, 01, 18)
            };
            Target.InsertReservation(reservation1);
            Assert.Throws<AutoUnavailableException>(() => Target.InsertReservation(reservation2));
        }

        [Fact]
        public void ContainsTest()
        {
            Reservation reservation1 = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };
            Reservation reservation2 = new Reservation
            {
                AutoId = 1,
                KundeId = 2,
                Von = new DateTime(2020, 01, 08),
                Bis = new DateTime(2020, 01, 22)
            };
            Target.InsertReservation(reservation1);
            Assert.Throws<AutoUnavailableException>(() => Target.InsertReservation(reservation2));
        }
    }
}
