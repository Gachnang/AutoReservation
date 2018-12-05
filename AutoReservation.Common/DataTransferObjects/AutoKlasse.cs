using System;
using System.ComponentModel;

namespace AutoReservation.Common.DataTransferObjects
{
    public enum AutoKlasse
    {
        [Description("Standard")]
        Standard,
        [Description("Mittelklasse")]
        Mittelklasse,
        [Description("Luxusklasse")]
        Luxusklasse
    }

    public static class AutoKlasseExtension {
        public static int ToInt(this AutoKlasse autoKlasse) {
            return (int) autoKlasse;
        }

        public static AutoKlasse ToAutoKlasse(this int value) {
            AutoKlasse ret = (AutoKlasse)Enum.ToObject(typeof(AutoKlasse), value);
            if (!Enum.IsDefined(typeof(AutoKlasse), ret))
            {
                throw new InvalidOperationException($"{value} is not an valid value of the AutoKlasse enumeration.");
            }

            return ret;
        }
    }
}
