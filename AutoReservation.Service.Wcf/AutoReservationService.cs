using System;
using System.Diagnostics;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService
    {
        /*
         * TODO: Implementation of IAutoReservationService
         */

        /*
         * TODO: Converting of DTO Objects to DAL Objects and the other way around
         */

        /*
         * TODO: Exception handling => catch known exceptions and pack it into faults
         */

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
    }
}