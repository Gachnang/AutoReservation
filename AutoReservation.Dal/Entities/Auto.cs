using System;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    public class Auto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Marke { get; set; }

        [Required]
        public int Tagestarif { get; set; }

        [Required]
        public int AutoKlasse { get; set; }

        public int BasisTarif { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
