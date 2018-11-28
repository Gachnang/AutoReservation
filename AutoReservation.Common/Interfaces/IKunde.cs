using System;

namespace AutoReservation.Common.Interfaces {
    public interface IKunde {

        int Id { get; set; }
        string Nachname { get; set; }
        string Vorname { get; set; }
        DateTime Geburtsdatum { get; set; }
        byte[] RowVersion { get; set; }
    }
}