using AutoReservation.Dal.Entities;
using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
	class AutoUnavailableException : Exception
	{
		public AutoUnavailableException(string message) : base(message) { }
		public AutoUnavailableException(string message, Reservation faultyReservation) : base(message)
		{
			this.faultyReservation = faultyReservation;
		}
		public Reservation faultyReservation { get; set; }
	}
}
