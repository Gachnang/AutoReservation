using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.BusinessLayer.Exceptions
{
    class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string message) : base(message) { }
        public InvalidDateRangeException(string message, Reservation faultyReservation) : base(message)
        {
            this.faultyReservation = faultyReservation;
        }

        public Reservation faultyReservation { get; set; }
    }
}
