using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Dal.Entities
{
    [Table("Customer", Schema = "dal")]
    public class Kunde : IKunde
    {
        [Column("Id", Order = 0)] [Required] [Key]
        public int Id { get; set; }

        [Column("Prename", Order = 1)] [Required]
        public string Nachname { get; set; }

        [Column("Name", Order = 2)] [Required]
        public string Vorname { get; set; }

        [Column("Birthday", Order = 3)]
        public DateTime Geburtsdatum { get; set; }

        [Column("RowVersion", Order = 4)] [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Reservation> Reservationen { get; } = new List<Reservation>();
    }
}
