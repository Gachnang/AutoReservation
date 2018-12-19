using System;
using AutoReservation.TestEnvironment;
using AutoReservation.Dal.Entities;
using AutoReservation.BusinessLayer.Exceptions;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void Exactly24hVonBisTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 02, 10, 10, 00, 00),
                Bis = new DateTime(2020, 02, 11, 10, 00, 00)
            };
            Target.InsertReservation(reservation);
            int count = Target.ListOfReservationen.Count;
            Reservation reservationDB = Target.GetReservation(count);
            Assert.Equal(reservation.Von , reservationDB.Von);
            Assert.Equal(reservation.Bis, reservationDB.Bis);
            Assert.Equal(reservation.AutoId, reservationDB.AutoId);
            Assert.Equal(reservation.KundeId, reservationDB.KundeId);
        }

        [Fact]
        public void SeveralDaysVonBisTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 02, 10),
                Bis = new DateTime(2020, 02, 20)
            };
            Target.InsertReservation(reservation);
            int count = Target.ListOfReservationen.Count;
            Reservation reservationDB = Target.GetReservation(count);
            Assert.Equal(reservation.Von, reservationDB.Von);
            Assert.Equal(reservation.Bis, reservationDB.Bis);
            Assert.Equal(reservation.AutoId, reservationDB.AutoId);
            Assert.Equal(reservation.KundeId, reservationDB.KundeId);
        }

        [Fact]
        public void VonAfterBisTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 20),
                Bis = new DateTime(2020, 01, 10)
            };
            Assert.Throws<InvalidDateRangeException>(() => Target.InsertReservation(reservation));
        }

        [Fact]
        public void LessThan24hVonBisTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10, 10, 00, 00),
                Bis = new DateTime(2020, 01, 11, 09, 59, 00)
            };
            Assert.Throws<InvalidDateRangeException>(() => Target.InsertReservation(reservation));
        }

        [Fact]
        public void SameDateVonBisTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10, 10, 00, 00),
                Bis = new DateTime(2020, 01, 10, 10, 00, 00)
            };
            Assert.Throws<InvalidDateRangeException>(() => Target.InsertReservation(reservation));
        }
    }
}
