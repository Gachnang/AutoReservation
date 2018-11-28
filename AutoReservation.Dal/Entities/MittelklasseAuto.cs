using System;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Dal.Entities
{
    public class MittelklasseAuto : Auto
    {
        public new AutoKlasse AutoKlasse
        {
            get => AutoKlasse.Standard;
            set => throw new AccessViolationException("Can not set AutoKlasse!");
        }

        public new int AutoKlasseId
        {
            get => AutoKlasse.Standard.ToInt();
            set => throw new AccessViolationException("Can not set AutoKlasseId!");
        }
    }
}
