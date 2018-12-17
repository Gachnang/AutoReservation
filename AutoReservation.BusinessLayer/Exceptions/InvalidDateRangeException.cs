using AutoReservation.Dal.Entities;
using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string message) : base(message) { }
        public InvalidDateRangeException(string message, Reservation faultyReservation) : base(message)
        {
            this.faultyReservation = faultyReservation;
        }

        public Reservation faultyReservation { get; set; }
    }
}
