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
        public void ScenarioOkay01Test()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10, 10, 00, 00),
                Bis = new DateTime(2020, 01, 11, 10, 00, 00)
            };
            Target.InsertReservation(reservation);
            int count = Target.ListOfReservationen.Count;
            Assert.Equal(reservation, Target.GetReservation(count - 1));
        }

        [Fact]
        public void ScenarioOkay02Test()
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
            Assert.Equal(reservation, Target.GetReservation(count - 1));
        }

        [Fact]
        public void ScenarioNotOkay01Test()
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
        public void ScenarioNotOkay02Test()
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
        public void ScenarioNotOkay03Test()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 01, 10, 10, 00, 00),
                Bis = new DateTime(2020, 01, 11, 10, 00, 00)
            };
            Assert.Throws<InvalidDateRangeException>(() => Target.InsertReservation(reservation));
        }
    }
}
