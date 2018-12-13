using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Dal.Entities
{
    [Table("Auto", Schema = "dbo")]
    public class Auto : IAuto {
        [Column("Id", Order = 0)] [Required] [Key]
        public int Id { get ; set; }
        
        [Column("Marke", Order = 1)] [MaxLength(20)] [Required]
        public string Marke { get; set; }

        [Column("Tagestarif", Order = 2)] [Required]
        public int Tagestarif { get; set; }

        //[Column("AutoKlassenId", Order = 3)] [Required]
        [NotMapped]
        public int AutoKlasseId { get; set; }

        [NotMapped]
        public AutoKlasse AutoKlasse {
            get {
                AutoKlasse ret = (AutoKlasse)Enum.ToObject(typeof(AutoKlasse), AutoKlasseId);
                if (!Enum.IsDefined(typeof(AutoKlasse), ret)) {
                    throw new InvalidOperationException(
                        $"{AutoKlasseId} is not an valid value of the AutoKlasse enumeration.");
                }

                return ret;
            }
            set {
                AutoKlasseId = (int)value;
            }
        }

        [Column("Basistarif", Order = 4)]
        public int Basistarif { get; set; }

        [Column("RowVersion", Order = 5)] [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Reservation> Reservationen { get; } = new List<Reservation>();
    }
}
