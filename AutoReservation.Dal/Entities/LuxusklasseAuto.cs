using System;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Dal.Entities
{
    public class LuxusklasseAuto : Auto
    {
        public AutoKlasse AutoKlasse
        {
            get => AutoKlasse.Standard;
            set => throw new AccessViolationException("Can not set AutoKlasse!");
        }

        public int AutoKlasseId
        {
            get => AutoKlasse.Standard.ToInt();
            set => throw new AccessViolationException("Can not set AutoKlasseId!");
        }
    }
}
