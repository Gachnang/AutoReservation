using System;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        public int ReservationNr { get; set; }

        public Auto Auto { get; set; }

        public Kunde Kunde { get; set; }

        public DateTime Von { get; set; }

        public DateTime Bis { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
