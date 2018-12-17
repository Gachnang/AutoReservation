using AutoReservation.Dal.Entities;
using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string message) : base(message) { }
        public InvalidDateRangeException(string message, Reservation faultyReservation) : base(message)
        {
            this.FaultyReservation = faultyReservation;
        }

        public Reservation FaultyReservation { get; set; }
    }
}
