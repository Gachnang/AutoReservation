using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservation", Schema = "dbo")]
    public class Reservation : IReservation
    {
        [Column("ReservationNo", Order = 0)] [Required] [Key]
        public int ReservationsNr { get; set; }

        [Column("AutoId", Order = 1)] [Required]
        public int AutoId { get; set; }

        [ForeignKey("AutoId")]
        public virtual IAuto Auto { get; set; }

        [Column("KundeId", Order = 2)] [Required]
        public int KundeId { get; set; }

        [ForeignKey("KundeId")]
        public virtual IKunde Kunde { get; set; }

        [Column("Von", Order = 3)] [Required]
        public DateTime Von { get; set; }

        [Column("Bis", Order = 4)]
        public DateTime Bis { get; set; }

        [Column("RowVersion", Order = 5)] [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
