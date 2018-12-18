using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Dal.Entities
{
    [Table("Kunde", Schema = "dbo")]
    public class Kunde
    {
        [Column("Id", Order = 0)] [Required] [Key]
        public int Id { get; set; }

        [Column("Nachname", Order = 1)] [Required]
        public string Nachname { get; set; }

        [Column("Vorname", Order = 2)] [Required]
        public string Vorname { get; set; }

        [Column("Geburtsdatum", Order = 3)]
        public DateTime Geburtsdatum { get; set; }

        [Column("RowVersion", Order = 4)] [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Reservation> Reservationen { get; } = new List<Reservation>();
    }
}
