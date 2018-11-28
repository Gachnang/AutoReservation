using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Dal.Entities
{
    [Table("Kunde", Schema = "dal")]
    public class Kunde : IKunde
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
    }
}
