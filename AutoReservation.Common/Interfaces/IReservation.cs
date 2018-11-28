using System;

namespace AutoReservation.Common.Interfaces {
    public interface IReservation {
        int ReservationsNr { get; set; }
        int AutoId { get; set; }
        IAuto Auto { get; set; }
        int KundeId { get; set; }
        IKunde Kunde { get; set; }
        DateTime Von { get; set; }
        DateTime Bis { get; set; }
        byte[] RowVersion { get; set; }
    }
}