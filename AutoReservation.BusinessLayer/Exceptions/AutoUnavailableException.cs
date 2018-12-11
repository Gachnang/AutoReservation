using System;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class AutoUnavailableException : Exception
    {
        public AutoUnavailableException(string message) : base(message) { }
        public AutoUnavailableException(string message, Auto AutoUnavailable) : base(message)
        {
            this.AutoUnavailable = AutoUnavailable;
        }

        public Auto AutoUnavailable { get; set; }
    }
}